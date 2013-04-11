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
    /// <summary>
    /// Interaction logic for ProfileContentPage.xaml
    /// </summary>
    public partial class ProfileContentPage : UserControl
    {
        public int State { get; set; }

        public ProfileContentPage()
        {
            InitializeComponent();
            State = 0;
        }

        private void Navigate_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                if (State > 0)
                {
                    MainContent.Children[State].Visibility = Visibility.Collapsed;
                    MainContent.Children[--State].Visibility = Visibility.Visible;
                }
                else
                {
                    var win = (MainWindow)Window.GetWindow(this);
                    win.profileContentPage.Visibility = Visibility.Collapsed;
                    win.profilePage.Visibility = Visibility.Visible;
                }
            }
            else if (e.Key == Key.Right)
            {
                if (State < MainContent.Children.Count - 1)
                {
                    MainContent.Children[State].Visibility = Visibility.Collapsed;
                    MainContent.Children[++State].Visibility = Visibility.Visible;
                }
                else
                {
                    var win = (MainWindow)Window.GetWindow(this);
                    win.profileContentPage.Visibility = Visibility.Collapsed;
                    win.profilePage.Visibility = Visibility.Visible;
                }
            }
            e.Handled = true;
        }
    }
}
