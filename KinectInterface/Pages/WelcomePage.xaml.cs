using System.Windows;
using System.Windows.Controls;

namespace KinectInterface.Pages 
{
    public partial class WelcomePage : UserControl 
    {
        public WelcomePage() 
        {
            InitializeComponent();
        }

        public void T_Pose(object sender, RoutedEventArgs e) {
            MainWindow win = (MainWindow)Window.GetWindow(this);
            win._welcomePage.Visibility = Visibility.Collapsed;
            win._homePage.Visibility = Visibility.Visible;
        }
    }
}
