using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client_WPF.DataModel;
using Client_WPF.FeedService;
using System.Windows.Input;
using Client_WPF.Utils;

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
        
        private List<RssFeed> rootFeeds = null;
        public List<RssFeed> RootFeeds
        {
            get { return rootFeeds; }
            private set { rootFeeds = value; RaisePropertyChange("RootFeeds"); }
        }

        private List<RssFeed> itemFeeds = null;
        public List<RssFeed> ItemFeeds
        {
            get { return itemFeeds; }
            private set { itemFeeds = value; RaisePropertyChange("ItemFeeds"); }
        }

        private string urlFeed = "";
        public string UrlFeed
        {
            get { return urlFeed; }
            set { urlFeed = value; RaisePropertyChange("UrlFeed"); }
        }

        private RssFeed currentFeed = null;
        public RssFeed CurrentFeed
        {
            get { return currentFeed; }
            set { currentFeed = value; RaisePropertyChange("CurrentFeed"); }
        }

        #endregion

        #region CTor
        public FeedsManagerViewModel()
        {
            FeedsManager = new FeedManagerDataModel();
            FeedsManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FeedsManager_PropertyChanged);

            RefreshFeeds = new RelayCommand((param) => FeedsManager.GetAllRootFeeds());
            AddFeed = new RelayCommand((param) => AddFeedBody());
            RemoveFeed = new RelayCommand((param) => FeedsManager.RemoveFeed(param as RssFeed));
            LoadFeedItems = new RelayCommand((param) => FeedsManager.LoadFeedItems(param as RssFeed));

            RootFeeds = new List<RssFeed>();
        }
        #endregion

        void FeedsManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "RootFeeds")
            {
                RootFeeds = (sender as FeedManagerDataModel).RootFeeds;
            }
            else if (e.PropertyName == "ItemFeeds")
                ItemFeeds = (sender as FeedManagerDataModel).ItemFeeds;
        }

        
        private void AddFeedBody()
        {
            FeedsManager.AddNewFeed(UrlFeed);
        }
    }
}
