using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace Client_WinPhone.View
{
    public partial class Channels : PhoneApplicationPage
    {
        public Channels()
        {
            InitializeComponent();
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.GoBack();
            //NavigationService.Navigate(new Uri("/View/Feeds.xaml", UriKind.Relative));
        }
    }
}