using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MahApps.Metro.Controls;
using Microsoft.Kinect;
using Microsoft.Samples.Kinect.SwipeGestureRecognizer;
using System.Windows.Threading;
namespace KinectInterface
{
	public enum states
    {
        Welcome = 0,
        Home = 1,
        Profil = 2,
        Game = 3,
        GameQuiz = 4,
        GameKtype = 5
    }

    public partial class MainWindow : MetroWindow
    {
        private IInputElement LeftQuizTarget;
        private IInputElement RightQuizTarget;

        private KinectSensor _KinectDevice;
        private Skeleton[] _FrameSkeletons;
        private Pose Tpose;
        private readonly Recognizer actRecognizer;
        private states state;
        private Instruction instruction;
        private DispatcherTimer instructionTimer;
        private int instructionSecond = 3;
        //instruction disabling
        private bool kinectDisable;
        public MainWindow() {
            InitializeComponent();
			EventManager.RegisterClassHandler(typeof(Window), Window.KeyDownEvent, new KeyEventHandler(AppHotkeyKeyDown));
            //bagian kinect
            createTpose();
            this.actRecognizer = this.CreateRecognizer();
            KinectSensor.KinectSensors.StatusChanged += KinectSensors_StatusChanged;
            this.KinectDevice = KinectSensor.KinectSensors.FirstOrDefault(x => x.Status == KinectStatus.Connected);
            state = states.Welcome;

            //timer for instruction
            instruction = new Instruction();
            kinectDisable = false;
            instructionTimer = new DispatcherTimer();
            instructionTimer.Tick += new EventHandler(this.instrucTime);
            instructionTimer.Interval = new TimeSpan(0, 0, instructionSecond );
        }

        //show instruction
        public void showInstruction(string comment)
        {
            instruction.txtA.Text = comment;
            instruction.Show();
            instructionTimer.Start();
            kinectDisable = true;   
        }
        //instruc event
        public void instrucTime(object sender, EventArgs e)
        {
            instructionTimer.Stop();
            kinectDisable = false;
            instruction.Hide();
            
        }
        //semua state
        public void changeState(states newState)
        {
            if (newState != state)
            {
                state = newState;
                switch (this.state)
                {
                    case states.Welcome:
                        
                        homePage.Visibility = Visibility.Collapsed;
                        gamePage.Visibility = Visibility.Collapsed;
                        aboutUsPage.Visibility = Visibility.Collapsed;
                        loginPage .Visibility = Visibility.Collapsed;
                        quizPage .Visibility = Visibility.Collapsed;
                        ktypePage.Visibility = Visibility.Collapsed;
                        profilePage.Visibility = Visibility.Collapsed;
                        welcomePage.Visibility = Visibility.Visible;
                        SkeletonViewerElement.isShowHand = false;
                        SkeletonViewerElement.Margin = new Thickness(254, 6, 182, 11);
                        SkeletonViewerElement.BorderThickness = new Thickness(0);
                        break;
                       
                    case states.Home:
                        showInstruction("home instruct");
                        homePage.Visibility = Visibility.Visible;
                        gamePage.Visibility = Visibility.Collapsed;
                        aboutUsPage.Visibility = Visibility.Collapsed;
                        loginPage .Visibility = Visibility.Collapsed;
                        quizPage .Visibility = Visibility.Collapsed;
                        ktypePage.Visibility = Visibility.Collapsed;
                        profilePage.Visibility = Visibility.Collapsed;
                        welcomePage.Visibility = Visibility.Collapsed;
                        SkeletonViewerElement.isShowHand = true;
                        break;

                    case states.Profil:
                        showInstruction("profil instruct");
                        break;
                    case states.Game:
                        showInstruction("game instruct");
                        break;
                    case states.GameQuiz :
                        showInstruction("gameQuiz instruct");
                        break;
                    case states .GameKtype :
                        showInstruction("ktype instruct");
                        SkeletonViewerElement.isShowHand = false ;
                        break;
                }
            }
        }

        //kinect
        private void KinectSensors_StatusChanged(object sender, StatusChangedEventArgs e)
        {
            switch (e.Status)
            {
                case KinectStatus.Initializing:
                case KinectStatus.Connected:
                case KinectStatus.NotPowered:
                case KinectStatus.NotReady:
                case KinectStatus.DeviceNotGenuine:
                    this._KinectDevice = e.Sensor;
                    break;
                case KinectStatus.Disconnected:
                    //TODO: Give the user feedback to plug-in a Kinect device.                    
                    this._KinectDevice = null;
                    break;
                default:
                    //TODO: Show an error state
                    break;
            }
        }

        private void KinectDevice_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            if (kinectDisable) return;
            using (SkeletonFrame frame = e.OpenSkeletonFrame())
            {
                if (frame != null)
                {
                    frame.CopySkeletonDataTo(this._FrameSkeletons);
                    Skeleton skeleton = GetPrimarySkeleton(this._FrameSkeletons);

                    if (skeleton == null)
                    {
                        changeState(states.Welcome);
                    }
                    else if (checkClippedEdge(skeleton))
                    {
                        SkeletonViewerElement.BorderThickness = new Thickness(0);
                        if (isTpose(skeleton))
                        {
                            //hide skeleton to view                           
                            SkeletonViewerElement.IsEnabled = false;
                            SkeletonViewerElement.Margin = new Thickness(0, 0, 0, 0);

                            welcomePage.T_Pose(null, null);
                            changeState(states.Home);
                            
                        }
                        else if(state == states .Welcome )
                        {
                            //show skeleton to view 
                            SkeletonViewerElement.IsEnabled = true ;
                            SkeletonViewerElement.Margin = new Thickness(254, 6, 182, 11);
                        }
                        else if (state == states.GameQuiz)
                        {
                            IInputElement leftTarget = GetHitTarget(skeleton.Joints[JointType.HandLeft], quizPage.QuizGrid);
                            IInputElement rightTarget = GetHitTarget(skeleton.Joints[JointType.HandRight], quizPage.QuizGrid);
                            bool hasTargetChange = (leftTarget != this.LeftQuizTarget) || (rightTarget != this.RightQuizTarget);
                            if (hasTargetChange)
                            {
                                for (int z = 0; z <= 5; z++)
                                {
                                    if (leftTarget != null && rightTarget != null)
                                    {
                                        
                                    }
                                    else if ((LeftQuizTarget == quizPage.QuizGrid.Children[z] && RightQuizTarget == null) ||
                                            (RightQuizTarget == quizPage.QuizGrid.Children[z] && LeftQuizTarget == null))
                                    {                                       
                                        quizPage.AnswerIs(z+1);
                                        break;
                                    }
                                    else if (leftTarget != null || rightTarget != null)
                                    {
                                        //Do nothing - target found
                                    }
                                    else
                                    {
                                        
                                    }
                                }

                                if (leftTarget != this.LeftQuizTarget)
                                {
                                    if (this.LeftQuizTarget != null)
                                    {
                                        ((FrameworkElement)this.LeftQuizTarget).Opacity = 0.2;
                                    }

                                    if (leftTarget != null)
                                    {
                                        ((FrameworkElement)leftTarget).Opacity = 1;
                                    }

                                    this.LeftQuizTarget = leftTarget;
                                }


                                if (rightTarget != this.RightQuizTarget)
                                {
                                    if (this.RightQuizTarget != null)
                                    {
                                        ((FrameworkElement)this.RightQuizTarget).Opacity = 0.2;
                                    }

                                    if (rightTarget != null)
                                    {
                                        ((FrameworkElement)rightTarget).Opacity = 1;
                                    }

                                    this.RightQuizTarget = rightTarget;
                                }
                            }
                            
                        }
                    }
                    
                    this.actRecognizer.Recognize(sender, frame, _FrameSkeletons);
                }
            }
        }
        public Boolean isTpose(Skeleton skel)
        {
            if (IsPose(skel, Tpose))
            {
                return true;
            }
            else return false;
        }
        public void createTpose()
        {
            Tpose = new Pose();
            Tpose.Title = "T";
            Tpose.Angles = new PoseAngle[4];
            Tpose.Angles[0] = new PoseAngle(JointType.ShoulderLeft, JointType.ElbowLeft, 180, 20);
            Tpose.Angles[1] = new PoseAngle(JointType.ElbowLeft, JointType.WristLeft, 180, 20);
            Tpose.Angles[2] = new PoseAngle(JointType.ShoulderRight, JointType.ElbowRight, 0, 20);
            Tpose.Angles[3] = new PoseAngle(JointType.ElbowRight, JointType.WristRight, 0, 20);
        }
        private static Point GetJointPoint(KinectSensor kinectDevice, Joint joint, Size containerSize, Point offset)
        {
            DepthImagePoint point = kinectDevice.MapSkeletonPointToDepth(joint.Position, kinectDevice.DepthStream.Format);
            point.X = (int)((point.X * containerSize.Width / kinectDevice.DepthStream.FrameWidth) - offset.X);
            point.Y = (int)((point.Y * containerSize.Height / kinectDevice.DepthStream.FrameHeight) - offset.Y);

            return new Point(point.X, point.Y);
        }
        private double GetJointAngle(Joint centerJoint, Joint angleJoint)
        {
            Point primaryPoint = GetJointPoint(this.KinectDevice, centerJoint, this.LayoutRoot.RenderSize, new Point());
            Point anglePoint = GetJointPoint(this.KinectDevice, angleJoint, this.LayoutRoot.RenderSize, new Point());
            Point x = new Point(primaryPoint.X + anglePoint.X, primaryPoint.Y);

            double a;
            double b;
            double c;

            a = Math.Sqrt(Math.Pow(primaryPoint.X - anglePoint.X, 2) + Math.Pow(primaryPoint.Y - anglePoint.Y, 2));
            b = anglePoint.X;
            c = Math.Sqrt(Math.Pow(anglePoint.X - x.X, 2) + Math.Pow(anglePoint.Y - x.Y, 2));

            double angleRad = Math.Acos((a * a + b * b - c * c) / (2 * a * b));
            double angleDeg = angleRad * 180 / Math.PI;

            if (primaryPoint.Y < anglePoint.Y)
            {
                angleDeg = 360 - angleDeg;
            }

            return angleDeg;
        }
        private bool IsPose(Skeleton skeleton, Pose pose)
        {
            bool isPose = true;
            double angle;
            double poseAngle;
            double poseThreshold;
            double loAngle;
            double hiAngle;

            for (int i = 0; i < pose.Angles.Length && isPose; i++)
            {
                poseAngle = pose.Angles[i].Angle;
                poseThreshold = pose.Angles[i].Threshold;
                angle = GetJointAngle(skeleton.Joints[pose.Angles[i].CenterJoint], skeleton.Joints[pose.Angles[i].AngleJoint]);

                hiAngle = poseAngle + poseThreshold;
                loAngle = poseAngle - poseThreshold;

                if (hiAngle >= 360 || loAngle < 0)
                {
                    loAngle = (loAngle < 0) ? 360 + loAngle : loAngle;
                    hiAngle = hiAngle % 360;

                    isPose = !(loAngle > angle && angle > hiAngle);
                }
                else
                {
                    isPose = (loAngle <= angle && hiAngle >= angle);
                }
            }

            return isPose;
        }

        public bool checkClippedEdge(Skeleton skeleton)
        {
            bool isInterrupt = true;
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Top))
            {
                isInterrupt = false;
                SkeletonViewerElement.BorderThickness = new Thickness(0, 12, 0, 0);
            }
            //else top.Text = "No";
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Bottom))
            {

            }
            //else bottom.Text = "No";
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Left))
            {
                isInterrupt = false;
                SkeletonViewerElement.BorderThickness = new Thickness(12, 0, 0, 0);
            }
            //else left.Text = "No";
            if (skeleton.ClippedEdges.HasFlag(FrameEdges.Right))
            {
                isInterrupt = false;
                SkeletonViewerElement.BorderThickness = new Thickness(0, 0, 12, 0);
            }
            //else right.Text = "No";
            return isInterrupt;
        }

        private static Skeleton GetPrimarySkeleton(Skeleton[] skeletons)
        {
            Skeleton skeleton = null;

            if (skeletons != null)
            {
                //Find the closest skeleton       
                for (int i = 0; i < skeletons.Length; i++)
                {
                    if (skeletons[i].TrackingState == SkeletonTrackingState.Tracked)
                    {
                        if (skeleton == null)
                        {
                            skeleton = skeletons[i];
                        }
                        else
                        {
                            if (skeleton.Position.Z > skeletons[i].Position.Z)
                            {
                                skeleton = skeletons[i];
                            }
                        }
                    }
                }
            }

            return skeleton;
        }

        //recognizer
        private Recognizer CreateRecognizer()
        {
            // Instantiate a recognizer.
            var recognizer = new Recognizer();

            // Wire-up swipe right to manually advance picture.
            recognizer.SwipeRightDetected += (s, e) =>
            {
                if (state == states.Home) homePage.GameShow();
                else if (state == states.Profil) profilePage.Right_Swipe(null, null);
                else if (state == states.Game) gamePage.GameKtype();
            };

            // Wire-up swipe left to manually reverse picture.
            recognizer.SwipeLeftDetected += (s, e) =>
            {
                if (state == states.Home) homePage.ProfileShow();
                else if (state == states.Profil) profilePage.Left_Swipe(null, null);
                else if (state == states.Game) gamePage.GameQuiz();
            };

            return recognizer;
        }


        //deteksi target hit
        private bool HitTest(Joint joint, UIElement target)
        {
            return (GetHitTarget(joint, target) != null);
        }
        private IInputElement GetHitTarget(Joint joint, UIElement target)
        {
            Point targetPoint = LayoutRoot.TranslatePoint(GetJointPoint(this.KinectDevice, joint, LayoutRoot.RenderSize, new Point()), target);
            return target.InputHitTest(targetPoint);
        }

        public KinectSensor KinectDevice
        {
            get { return this._KinectDevice; }
            set
            {
                if (this._KinectDevice != value)
                {
                    //Uninitialize
                    if (this._KinectDevice != null)
                    {
                        this._KinectDevice.Stop();
                        this._KinectDevice.SkeletonFrameReady -= KinectDevice_SkeletonFrameReady;
                        this._KinectDevice.SkeletonStream.Disable();

                        this._FrameSkeletons = null;
                    }

                    this._KinectDevice = value;

                    //Initialize
                    if (this._KinectDevice != null)
                    {
                        if (this._KinectDevice.Status == KinectStatus.Connected)
                        {
                            this._KinectDevice.SkeletonStream.Enable();
                            this._FrameSkeletons = new Skeleton[this._KinectDevice.SkeletonStream.FrameSkeletonArrayLength];
                            this._KinectDevice.Start();

                            SkeletonViewerElement.KinectDevice = this._KinectDevice;
                            this._KinectDevice.SkeletonFrameReady += KinectDevice_SkeletonFrameReady;
                        }
                    }
                }
            }
        }

        private void AppHotkeyKeyDown(object source, KeyEventArgs e)
        {
            if (e.Key == Key.Home)
            {
                LayoutRoot.Children.Cast<UserControl>().ToList().ForEach(x => x.Visibility = Visibility.Collapsed);
                homePage.Visibility = Visibility.Visible;
                profilePage.Reset();
            }
            else if (e.Key == Key.End)
            {
                LayoutRoot.Children.Cast<UserControl>().ToList().ForEach(x => x.Visibility = Visibility.Collapsed);
                welcomePage.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.F1)
            {
                LayoutRoot.Children.Cast<UserControl>().ToList().ForEach(x => x.Visibility = Visibility.Collapsed);
                aboutUsPage.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.Space)
            {
                Flyouts[0].IsOpen = !Flyouts[0].IsOpen;
            }
        }
    }
}
