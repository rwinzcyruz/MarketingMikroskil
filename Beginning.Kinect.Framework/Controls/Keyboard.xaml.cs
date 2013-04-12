using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Beginning.Kinect.Framework.Controls
{
    /// <summary>
    /// Interaction logic for Keyboard.xaml
    /// </summary>
    public partial class Keyboard : UserControl
    {
        TextBox  textBox;
        
        public Keyboard()
        {
            InitializeComponent();
        }
        public void setBlock(TextBox   textB)
        {
            this.textBox = textB;
        }
        public void DoSomething(object sender, RoutedEventArgs e)
        {
            HoverButton button = (HoverButton)e.Source;
            textBox.Text = (String)button.Content;
        }
        
    }
}
