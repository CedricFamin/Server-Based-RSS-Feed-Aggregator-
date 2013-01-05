using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;

namespace Common.Behavior
{
    public class ToggleVisibilityBehavior : Behavior<Button>
    {
        public bool IsVisible
        {
            get { return (bool)GetValue(IsVisibleProperty); }
            set { SetValue(IsVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsVisibleProperty =
            DependencyProperty.Register("IsVisible", typeof(bool), typeof(ToggleVisibilityBehavior), null);



        public UIElement Target
        {
            get { return (UIElement)GetValue(TargetProperty); }
            set { SetValue(TargetProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Target.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetProperty =
            DependencyProperty.Register("Target", typeof(UIElement), typeof(ToggleVisibilityBehavior), null);

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Click += new RoutedEventHandler(AssociatedObject_Click);
        }

        void AssociatedObject_Click(object sender, RoutedEventArgs e)
        {
            IsVisible = !IsVisible;
            Target.Visibility = IsVisible ? Visibility.Visible : Visibility.Collapsed;
            
        }
    }
}
