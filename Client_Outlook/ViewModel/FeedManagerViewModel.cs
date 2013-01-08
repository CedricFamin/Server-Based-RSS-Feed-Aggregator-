﻿using System;
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
        #endregion

        #region Commands
        public ICommand RemoveFeed { get; private set; }
        #endregion

        #region CTor
        public FeedManagerViewModel()
        {
            FeedManager = new FeedManagerDataModel();
            FeedManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FeedManager_PropertyChanged);
            Instance = this;

            RemoveFeed = new RelayCommand((param) => FeedManager.RemoveFeed(param as Channel));
        }
        #endregion

        #region Property changed
        void FeedManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Channels")
            {
                Channels = FeedManager.Channels;
            }

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