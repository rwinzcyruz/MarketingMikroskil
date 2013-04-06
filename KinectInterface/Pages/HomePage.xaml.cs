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

            win.homePage.Visibility = Visibility.Collapsed;
            switch (btn.Name) 
            {
                case "_btnProfile": win.profilePage.Visibility = Visibility.Visible; break;
                case "_btnGame": win.gamePage.Visibility = Visibility.Visible; break;
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
            win.gamePage.Visibility = Visibility.Visible;

            win.changeState(states.Game);
        }
    }
}
