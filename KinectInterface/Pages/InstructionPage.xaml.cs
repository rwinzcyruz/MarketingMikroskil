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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KinectInterface.Pages
{
    public partial class InstructionPage : UserControl
    {
        private DispatcherTimer idleTimer;
        
        public InstructionPage()
        {
            InitializeComponent();
            //idleTimer = new DispatcherTimer();
            //idleTimer.Tick += new EventHandler(this.idleTime);
            //idleTimer.Interval = new TimeSpan(0, 0, 3);
        }
        
        //public void idleTime(object sender, EventArgs e)
        //{
        //    idleTimer.Stop();
        //    this.Close();
        //}

        //private void Window_Loaded_1(object sender, RoutedEventArgs e)
        //{
        //    idleTimer.Start();
        //}       
    }
}
