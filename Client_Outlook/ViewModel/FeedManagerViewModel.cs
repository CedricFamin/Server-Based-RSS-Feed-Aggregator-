using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Utils;
using Common.DataModel;
using Common.FeedService;
using System.Windows.Input;

namespace Client_Outlook.ViewModel
{
    class FeedManagerViewModel : BindableObject
    {
        static public FeedManagerViewModel Instance { get; private set; }

        #region DataModel
        private FeedManagerDataModel FeedManager { get; set; }

        private List<Channel> _channels;

        public List<Channel> Channels
        {
            get { return _channels; }
            set { _channels = value; RaisePropertyChange("Channels");  }
        }

        private List<Channel> _allChannels;

        public List<Channel> AllChannels
        {
            get { return _allChannels; }
            set { _allChannels = value; RaisePropertyChange("AllChannels"); }
        }
        #endregion

        #region Commands
        public ICommand RemoveFeed { get; private set; }
        public ICommand AddFollowFeed { get; private set; }
        #endregion

        #region CTor
        public FeedManagerViewModel()
        {
            FeedManager = FeedManagerDataModel.Instance;
            FeedManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FeedManager_PropertyChanged);
            Instance = this;

            RemoveFeed      = new RelayCommand((param) => FeedManager.RemoveFeed(param as Channel));
            AddFollowFeed   = new RelayCommand((param) => FeedManager.AddNewFeed((param as Channel).Url));
        }
        #endregion

        #region Property changed
        void FeedManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Channels":
                    Channels = FeedManager.Channels;
                    break;
                case "AllChannels":
                    AllChannels = FeedManager.AllChannels;
                    break;
            };
        }
        #endregion

        public void GetAllRootFeeds()
        {
            FeedManager.GetAllRootFeeds();
        }

        public FeedDetailsViewModel GetChannelDetails(Channel channel)
        {
            return new FeedDetailsViewModel(channel);
        }
    }
}
