using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Interactivity;
using System.Windows.Controls;
using System.Windows;
using Microsoft.Phone.Controls;

namespace Client_WinPhone.Behavior
{
    class WebBrowserUtilityBehavior : Behavior<WebBrowser>
    {
        public string HtmlContent
        {
            get { return (string)GetValue(HtmlContentProperty); }
            set { SetValue(HtmlContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HtmlContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HtmlContentProperty =
            DependencyProperty.Register("HtmlContent", typeof(string), typeof(WebBrowserUtilityBehavior), null);

        protected override void OnAttached()
        {
            base.OnAttached();
            //AssociatedObject.Initialized += new EventHandler(AssociatedObject_Initialized);
            AssociatedObject.LoadCompleted += new System.Windows.Navigation.LoadCompletedEventHandler(AssociatedObject_LoadCompleted);
        }

        void AssociatedObject_LoadCompleted(object sender, System.Windows.Navigation.NavigationEventArgs e)
        {/*
            mshtml.HTMLDocument document = AssociatedObject.Document as mshtml.HTMLDocument;

            if (document != null && document.body != null)
            {
                mshtml.IHTMLElement2 body = (mshtml.IHTMLElement2)document.body;
                AssociatedObject.Height = body.scrollHeight;
                document.body.style.overflow = "hidden";
            }
            */
        }

        void AssociatedObject_Initialized(object sender, EventArgs e)
        {
            AssociatedObject.NavigateToString(HtmlContent);
        }
    }
}
