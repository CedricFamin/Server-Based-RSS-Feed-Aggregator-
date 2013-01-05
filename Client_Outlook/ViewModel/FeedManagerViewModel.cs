using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Utils;
using Common.DataModel;
using Common.FeedService;

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

        #region CTor
        public FeedManagerViewModel()
        {
            FeedManager = new FeedManagerDataModel();
            FeedManager.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(FeedManager_PropertyChanged);
            Instance = this;
        }
        #endregion

        #region Property changed
        void FeedManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Channels")
            {
                Channels = (sender as FeedManagerDataModel).Channels;
            }

        }
        #endregion

        public void GetAllRootFeeds()
        {
            FeedManager.GetAllRootFeeds();
        }
    }
}
