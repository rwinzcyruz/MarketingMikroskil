
using System.Windows;

namespace Beginning.Kinect.Framework.Controls
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Windows.Controls;
    using Beginning.Kinect.Framework;
    using System.Windows.Threading;
    using Beginning.Kinect.Framework.Input;
    using System.Diagnostics;

    public class HoverTextBox : KinectTextBox
    {
        readonly DispatcherTimer _hoverTimer = new DispatcherTimer();
        protected bool _timerEnabled = true;

        /// <summary>
        /// Gets or sets the amount of time required for a hover to trigger the click event.
        /// </summary>
        /// <value>
        /// The hover interval.
        /// </value>
        public double TextHoverInterval
        {
            get { return (double)GetValue(HoverIntervalProperty); }
            set { SetValue(HoverIntervalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HoverInterval.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HoverIntervalProperty =
            DependencyProperty.Register("TextHoverInterval", typeof(double), typeof(HoverButton), new UIPropertyMetadata(2000d));


        public HoverTextBox()
        {

            _hoverTimer.Interval = TimeSpan.FromMilliseconds(TextHoverInterval);
            _hoverTimer.Tick += _hoverTimer_Tick;
            _hoverTimer.Stop();
        }


        

        override protected void OnKinectCursorEnter(object sender, KinectCursorEventArgs e)
        {
            
            if (_timerEnabled)
            {
                _hoverTimer.Interval = TimeSpan.FromMilliseconds(TextHoverInterval);
                e.Cursor.AnimateCursor(TextHoverInterval);
                _hoverTimer.Start();
            }
        }


        override protected void OnKinectCursorLeave(object sender, KinectCursorEventArgs e)
        {
            if (_timerEnabled)
            {
                e.Cursor.StopCursorAnimation();
                _hoverTimer.Stop();
            }
          
        }

        void _hoverTimer_Tick(object sender, EventArgs e)
        {
            _hoverTimer.Stop();
            RaiseEvent(new RoutedEventArgs(GotFocusEvent));
        }

    }
}
