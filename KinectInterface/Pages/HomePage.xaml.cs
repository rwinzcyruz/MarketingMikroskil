using System.Windows;
using System.Windows.Controls;

namespace KinectInterface.Pages 
{
    public partial class HomePage : UserControl 
    {
        public HomePage() 
        {
            InitializeComponent();
        }

        private void Navigate_Click(object sender, RoutedEventArgs e) 
        {
            var btn = (Button)e.OriginalSource;
            var win = (MainWindow)Window.GetWindow(this);

            
            switch (btn.Name) 
            {
                case "_btnProfile": ProfileShow();
                    break;
                case "_btnGame": GameShow();
                    break;
            }
        }

		public void ProfileShow()
        {
            var win = (MainWindow)Window.GetWindow(this);
            win.homePage.Visibility = Visibility.Collapsed;
            win.profilePage.Visibility = Visibility.Visible;

            win.changeState(states.Profil);
        }
        public void GameShow()
        {
            var win = (MainWindow)Window.GetWindow(this);
            win.homePage.Visibility = Visibility.Collapsed;
            //ubah login ni
            win.loginPage.Visibility = Visibility.Visible;
            win.changeState(states.Login);
            //win.gamePage.Visibility = Visibility.Visible;
            //win.changeState(states.Game);

        }
    }
}
