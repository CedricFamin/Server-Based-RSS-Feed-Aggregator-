using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DataModel;
using Common.FeedService;
using System.Windows.Input;
using Common.Utils;

namespace Client_WPF.ViewModel
{
    class FeedsManagerViewModel : BindableObject
    {
        private FeedManagerDataModel FeedsManager { get; set; }

        #region properties
        public ICommand RefreshFeeds { get; private set; }
        public ICommand AddFeed { get; private set; }
        public ICommand RemoveFeed { get; private set; }
        public ICommand LoadFeedItems { get; private set; }
        public ICommand OpenFeedDetails { get; private set; }
        
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
        public FeedsManagerViewModel()
        {
            FeedsManager = FeedManagerDataModel.Instance;
            FeedsManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FeedsManager_PropertyChanged);

            RefreshFeeds = new RelayCommand((param) => FeedsManager.GetAllRootFeeds());
            AddFeed = new RelayCommand((param) => AddFeedBody(param as string));
            RemoveFeed = new RelayCommand((param) => FeedsManager.RemoveFeed(param as Channel));
            LoadFeedItems = new RelayCommand((param) => FeedsManager.LoadFeedItems(param as Channel));

            OpenFeedDetails = new RelayCommand((param) => (new ChannelDetails()
            {
                DataContext = new FeedDetailsViewModel(param as Channel)
            }).Show());

            Channels = new List<Channel>();

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
    }
}
