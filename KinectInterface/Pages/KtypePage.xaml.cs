using System;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
namespace KinectInterface.Pages
{
    public partial class KtypePage : UserControl
    {
        private int live;
        private int score;
        private string hrf;
        private int time;
        private DispatcherTimer idleTimer;
        public KtypePage()
        {
            InitializeComponent();
            idleTimer = new DispatcherTimer();
            idleTimer.Tick += new EventHandler(this.idleTime);
            idleTimer.Interval = new TimeSpan(0, 0, 1);
            this.keyboard.setBlock(txtPenampung);
        }
        public void idleTime(object sender, EventArgs e)
        {
            time -= 1;
            txtBTime.Text = time.ToString();
            if (time == 0)
            {
                GameOver();
            }
        }
        public void cekJawaban(string huruf)
        {
            if (huruf.Equals(hrf))
            {
                score += 10;
                reload();
            }
            else
            {
                live--;
                if (live == 0) GameOver();
                else reload();
            }
        }

        private void txtPenampung_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox txt = (TextBox)e.Source;
            cekJawaban((String)txt.Text);
        }

        public void Start()
        {
            live = 3;
            score = 0;
            time = 60;
            idleTimer.Start();
            reload();
        }

        private void reload()
        {
            hrf = hurufAcak(1);
            txtBlive.Text = live.ToString();
            txtBscore.Text = score.ToString();
            txtBhuruf.Text = hrf.ToString();
            txtBTime.Text = time.ToString();
        }

        public void GameOver()
        {
            idleTimer.Stop();
            var win = (MainWindow)Window.GetWindow(this);
            win.ktypePage.Visibility = Visibility.Collapsed;
            win.homePage.Visibility = Visibility.Visible;
            win.changeState(states.Home );
        }

        public String hurufAcak(int length)
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
    }
}
