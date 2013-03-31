using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                case "_btnPlay": win.playPage.Visibility = Visibility.Visible; break;
            }
        }
    }
}
