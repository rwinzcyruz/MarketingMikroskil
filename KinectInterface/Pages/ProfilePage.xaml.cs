using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Linq;
using System.Windows.Media.Imaging;

namespace KinectInterface.Pages 
{
    public partial class ProfilePage : UserControl 
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private void Navigate_Click(object sender, RoutedEventArgs e)
        {
            var btn = (Button)e.OriginalSource;
            var win = (MainWindow)Window.GetWindow(this);
            win.profilePage.Visibility = Visibility.Collapsed;
            win.profileContentPage.Visibility = Visibility.Visible;

            int size = 0;
            switch (btn.Content.ToString())
            {
                case "TI": size = 2; break;
                case "SI": size = 2; break;
                case "MA": size = 2; break;
                case "AK": size = 2; break;
                case "MI": size = 2; break;
                case "KA": size = 2; break;
            }
            win.profileContentPage.MainContent.Children.Clear();
            win.profileContentPage.State = 0;
            Generate(btn.Content.ToString(), size, btn.Tag.ToString());

        }

        private void Generate(string name, int page, string content)
        {
            var win = (MainWindow)Window.GetWindow(this);
            for (int i = 1; i <= page; i++)
            {
                var bmp = new BitmapImage(new System.Uri("pack://application:,,,/KinectInterface;component/Images/" + name + " (" + i + ").png"));
                var img = new Image();
                img.Source = bmp;
                img.Stretch = Stretch.Fill;

                if (i != 1) img.Visibility = Visibility.Collapsed;

                win.profileContentPage.TitleText.Content = content;
                win.profileContentPage.MainContent.Children.Add(img);
            }
        }

        //#region Keyboard Navigation

        //private void Arrow_PreviewKeyDown(object sender, RoutedEventArgs e)
        //{
        //    if (((KeyEventArgs)e).Key == Key.Left)
        //    {
        //        if (state > 0)
        //        {
        //            ProfileGrid.Children[state].Visibility = Visibility.Collapsed;
        //            ProfileGrid.Children[--state].Visibility = Visibility.Visible;
        //        }
        //        else
        //        {
        //            MainWindow win = (MainWindow)Window.GetWindow(this);
        //            win.profilePage.Visibility = Visibility.Collapsed;
        //            win.homePage.Visibility = Visibility.Visible;
        //        }
        //        e.Handled = true;
        //    }
        //    else if (((KeyEventArgs)e).Key == Key.Right)
        //    {
        //        if (state < ProfileGrid.Children.Count - 1)
        //        {
        //            ProfileGrid.Children[state].Visibility = Visibility.Collapsed;
        //            ProfileGrid.Children[++state].Visibility = Visibility.Visible;
        //        }
        //        else
        //        {
        //            Reset();
        //            MainWindow win = (MainWindow)Window.GetWindow(this);
        //            win.profilePage.Visibility = Visibility.Collapsed;
        //            win.homePage.Visibility = Visibility.Visible;
        //        }
        //        e.Handled = true;
        //    }
        //}

        //#endregion

        //#region Kinect Navigation

        //public void Left_Swipe(object sender, RoutedEventArgs e)
        //{
        //    if (state > 0)
        //    {
        //        ProfileGrid.Children[state].Visibility = Visibility.Collapsed;
        //        ProfileGrid.Children[--state].Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        MainWindow win = (MainWindow)Window.GetWindow(this);
        //        win.profilePage.Visibility = Visibility.Collapsed;
        //        win.homePage.Visibility = Visibility.Visible;
        //        win.changeState(states.Home);
        //    }
        //}

        //public void Right_Swipe(object sender, RoutedEventArgs e)
        //{
        //    if (state < ProfileGrid.Children.Count - 1)
        //    {
        //        ProfileGrid.Children[state].Visibility = Visibility.Collapsed;
        //        ProfileGrid.Children[++state].Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        Reset();
        //        MainWindow win = (MainWindow)Window.GetWindow(this);
        //        win.profilePage.Visibility = Visibility.Collapsed;
        //        win.homePage.Visibility = Visibility.Visible;
        //        win.changeState(states.Home);
        //    }
        //}

        //#endregion
    }
}
