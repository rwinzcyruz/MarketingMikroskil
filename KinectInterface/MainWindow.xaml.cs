using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using System.Linq;
using MahApps.Metro.Controls;

namespace KinectInterface
{
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            EventManager.RegisterClassHandler(typeof(Window), Window.KeyDownEvent, new KeyEventHandler(AppHotkeyKeyDown));
        }

        private void AppHotkeyKeyDown(object source, KeyEventArgs e)
        {
            if (e.Key == Key.Home)
            {
                mainGrid.Children.Cast<UserControl>().ToList().ForEach(x => x.Visibility = Visibility.Collapsed);
                homePage.Visibility = Visibility.Visible;
                profilePage.Reset();
            }
            else if (e.Key == Key.End)
            {
                mainGrid.Children.Cast<UserControl>().ToList().ForEach(x => x.Visibility = Visibility.Collapsed);
                welcomePage.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.F1)
            {
                mainGrid.Children.Cast<UserControl>().ToList().ForEach(x => x.Visibility = Visibility.Collapsed);
                aboutUsPage.Visibility = Visibility.Visible;
            }
            else if (e.Key == Key.Space)
            {
                Flyouts[0].IsOpen = !Flyouts[0].IsOpen;
            }
        }
    }
}