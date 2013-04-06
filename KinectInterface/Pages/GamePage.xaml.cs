using System.Windows;
using System.Windows.Controls;

namespace KinectInterface.Pages 
{
    public partial class GamePage : UserControl 
    {
        public GamePage() 
        {
            InitializeComponent();
        }

        private void Navigate_Click(object sender, RoutedEventArgs e) 
        {
            var btn = (Button)e.OriginalSource;
            var win = (MainWindow)Window.GetWindow(this);

            win.gamePage.Visibility = Visibility.Collapsed;
            switch (btn.Name) 
            {
                case "_btnQuiz": win.loginPage.Visibility = Visibility.Visible; break;
                case "_btnPlay": win.ktypePage.Visibility = Visibility.Visible; break;
            }
        }

		public void GameQuiz()
        {
            var win = (MainWindow)Window.GetWindow(this);
            win.gamePage.Visibility = Visibility.Collapsed;
            win.loginPage.Visibility = Visibility.Visible;
            //ingat balikin
            //win.changeState(states.GameQuiz);
            //win.loginPage.toGameQuiz();
        }
        public void GameKtype()
        {
            var win = (MainWindow)Window.GetWindow(this);
            win.gamePage.Visibility = Visibility.Collapsed;
            
            win.ktypePage.Visibility = Visibility.Visible;

            win.changeState(states.GameKtype);
            win.ktypePage.Start();
        }
    }
}
