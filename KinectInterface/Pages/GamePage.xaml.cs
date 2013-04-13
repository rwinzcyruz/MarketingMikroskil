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

            
            switch (btn.Name) 
            {
                case "_btnQuiz": GameQuiz(); break;
                case "_btnPlay": GameKtype(); break;
            }
        }

		public void GameQuiz()
        {
            var win = (MainWindow)Window.GetWindow(this);
            win.gamePage.Visibility = Visibility.Collapsed;
            win.quizPage.Visibility = Visibility.Visible;
            win.quizPage.CreateQuestion();
            win.quizPage.QuizReset();
            win.changeState(states.GameQuiz);
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
