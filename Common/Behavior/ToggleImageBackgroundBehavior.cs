using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Common.Behavior
{
    public class ToggleImageBackgroundBehavior : Behavior<Button>
    {
        public ImageSource BGImageOn
        {
            get { return (ImageSource)GetValue(BGImage1Property); }
            set { SetValue(BGImage1Property, value); }
        }

        // Using a DependencyProperty as the backing store for BGImage1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BGImage1Property =
            DependencyProperty.Register("BGImage1", typeof(ImageSource), typeof(ToggleImageBackgroundBehavior), null);

        public ImageSource BGImageOff
        {
            get { return (ImageSource)GetValue(BGImage2Property); }
            set { SetValue(BGImage2Property, value); }
        }

        // Using a DependencyProperty as the backing store for BGImage2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BGImage2Property =
            DependencyProperty.Register("BGImage2", typeof(ImageSource), typeof(ToggleImageBackgroundBehavior), null);

        public bool IsOn
        {
            get { return (bool)GetValue(IsOnProperty); }
            set { SetValue(IsOnProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOnProperty =
            DependencyProperty.Register("IsOn", typeof(bool), typeof(ToggleImageBackgroundBehavior), null);

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += new RoutedEventHandler(AssociatedObject_Click);
            AssociatedObject.Background = new ImageBrush(IsOn ? BGImageOn : BGImageOff);
        }

        void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            IsOn = !IsOn;
            AssociatedObject.Background = new ImageBrush(IsOn ? BGImageOn : BGImageOff);
        }
    }
}
