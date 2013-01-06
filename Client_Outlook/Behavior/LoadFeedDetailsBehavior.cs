using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows;
using Common.FeedService;
using Client_Outlook.ViewModel;

namespace Client_Outlook.Behavior
{
    class LoadFeedDetailsBehavior : Behavior<FrameworkElement>
    {
        public object Destination
        {
            get { return (object)GetValue(DestinationProperty); }
            set { SetValue(DestinationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Destination.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DestinationProperty =
            DependencyProperty.Register("Destination", typeof(object), typeof(LoadFeedDetailsBehavior), null);

        public Channel BaseChannel
        {
            get { return (Channel)GetValue(BaseChannelProperty); }
            set { SetValue(BaseChannelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BaseChannel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BaseChannelProperty =
            DependencyProperty.Register("BaseChannel", typeof(Channel), typeof(LoadFeedDetailsBehavior), null);

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.Initialized += new EventHandler(AssociatedObject_Initialized);
        }

        void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            if (BaseChannel != null)
            {
                Destination = new FeedDetailsViewModel(BaseChannel);
                AssociatedObject.DataContext = Destination;
            }
        }
    }
}
