using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Common.DataModel;
using System.ComponentModel;
using Common.Utils;
using System.Windows;
using System.Windows.Navigation;
using System.Diagnostics;
using Microsoft.Phone.Controls;
using Client_WinPhone.FeedService;
using Client_WinPhone.View;

namespace Client_WinPhone.ViewModel
{
    public class FeedsPageViewModel : BindableObject
    {
        private FeedManagerDataModel FeedsManager { get; set; }

        #region properties
        public ICommand RefreshFeed { get; private set; }
        public ICommand RefreshFeeds { get; private set; }
        public ICommand AddFeed { get; private set; }
        public ICommand RemoveFeed { get; private set; }
        public ICommand LoadFeedItems { get; private set; }
        public ICommand OpenFeedDetails { get; private set; }
        public ICommand LogOut { get; private set; }
        
        private List<Channel> channels = null;
        public List<Channel> Channels
        {
            get { return channels; }
            private set { channels = value; RaisePropertyChange("Channels"); }
        }

        private List<Channel> allChannels = null;
        public List<Channel> AllChannels
        {
            get { return allChannels; }
            private set { allChannels = value; RaisePropertyChange("AllChannels"); }
        }

        private List<Item> items = null;
        public List<Item> Items
        {
            get { return items; }
            private set { items = value; RaisePropertyChange("Items"); }
        }

        private string urlFeed = "";
        public string UrlFeed
        {
            get { return urlFeed; }
            set { urlFeed = value; RaisePropertyChange("UrlFeed"); }
        }

        private Channel currentChannel = null;
        public Channel CurrentChannel
        {
            get { return currentChannel; }
            set { currentChannel = value; RaisePropertyChange("CurrentChannel"); }
        }

        #endregion

        #region CTor
        public FeedsPageViewModel()
        {
            FeedsManager = FeedManagerDataModel.Instance;
            FeedsManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FeedsManager_PropertyChanged);

            RefreshFeeds = new RelayCommand((param) => FeedsManager.GetAllRootFeeds());
            RefreshFeed = new RelayCommand((param) => FeedsManager.RefreshFeed(param as Channel));
            AddFeed = new RelayCommand((param) => AddFeedBody(param as string));
            RemoveFeed = new RelayCommand((param) => FeedsManager.RemoveFeed(param as Channel));
            LoadFeedItems = new RelayCommand((param) => FeedsManager.LoadFeedItems(param as Channel));

            /*OpenFeedDetails = new RelayCommand((param) => (new ChannelFeedsPage()
            {
                DataContext = new FeedDetailsViewModel(param as Channel)
            }));*/
            OpenFeedDetails = new RelayCommand((param) => OpenDetails(param as Channel));
            //LogOut = new RelayCommand((param) => Logout(param as string[]));

            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            bool success = frame.Navigate(new Uri("/View/DetailedFeedsPage.xaml", UriKind.Relative));

            Channels = new List<Channel>();
            FeedsManager.GetAllRootFeeds();
            SearchDataModel.Instance.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(Instance_PropertyChanged);
        }

        void Instance_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Search":
                    SearchDataModel searchInstance = (sender as SearchDataModel);
                    string searchValue = searchInstance.Search;
                    if (searchInstance.IsSearchValue(searchValue))
                    {
                        AllChannels = (from chan in FeedManagerDataModel.Instance.AllChannels where chan.Title.Contains(searchValue, StringComparison.OrdinalIgnoreCase) select chan).ToList();
                        Channels = (from chan in FeedManagerDataModel.Instance.Channels where chan.Title.Contains(searchValue) select chan).ToList();
                    }
                    else
                    {
                        AllChannels = FeedManagerDataModel.Instance.AllChannels;
                        Channels = FeedManagerDataModel.Instance.Channels;
                    }
                        
                    break;
            }
        }
        #endregion

        private void OpenDetails(Channel param)
        {
            Debug.WriteLine("Opening : ");
            new ChannelFeedsPage()
            {
                DataContext = new FeedDetailsViewModel(param)
            };
            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            bool success = frame.Navigate(new Uri("/View/DetailedFeedsPage.xaml", UriKind.Relative));

        }

        void FeedsManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Channels")
            {
                Channels = (sender as FeedManagerDataModel).Channels;
            }
            else if (e.PropertyName == "Items")
                Items = (sender as FeedManagerDataModel).Items;
            else if (e.PropertyName == "AllChannels")
                AllChannels = (sender as FeedManagerDataModel).AllChannels;
        }
        
        private void AddFeedBody(string url)
        {
            FeedsManager.AddNewFeed(url);
        }

        private void Logout(object sender, EventArgs e)
        {
            UserDataModel.Instance.Logout();
            PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
            //bool success = frame.Navigate(new Uri("/View/LogInPage.xaml", UriKind.Relative));
            frame.GoBack();
        }
    }
}
