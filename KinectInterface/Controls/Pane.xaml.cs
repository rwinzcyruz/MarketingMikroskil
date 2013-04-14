using System.Windows;
using System.Windows.Controls;

namespace KinectInterface.Controls
{
    public partial class Pane : UserControl
    {
        public static readonly DependencyProperty LabelProperty =
            DependencyProperty.Register("Label", typeof(string), typeof(Pane), new PropertyMetadata(default(string)));
        public static readonly DependencyProperty CountProperty =
            DependencyProperty.Register("Value", typeof(string), typeof(Pane), new PropertyMetadata(default(string)));

        public string Label { get; set; }
        public string Value {
            get { return (string)GetValue(CountProperty); }
            set { SetValue(CountProperty, value); }
        }

        public Pane()
        {
            InitializeComponent();
        }
    }
}
