using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client_WPF.FeedService;
using System.ServiceModel;
using Client_WPF.ViewModel;

namespace Client_WPF.DataModel
{
    class FeedManagerDataModel : BindableObject
    {
        #region Common
        static private FeedsServiceClient feedsClient = null;
        static private FeedsServiceClient FeedsClient
        {
            get
            {
                if (feedsClient == null)
                    feedsClient = new FeedsServiceClient();
                if (feedsClient.State == CommunicationState.Closed)
                    feedsClient.Open();
                return feedsClient;
            }
        }
        #endregion

        #region PPties
        private UserDataModel UserData { get; set; }

        private List<Channel> channels = null;
        public List<Channel> Channels 
        {
            get { return channels; }
            private set { channels = value; RaisePropertyChange("Channels"); }
        }

        private List<Item> item = null;
        public List<Item> Items
        {
            get { return item; }
            private set { item = value; RaisePropertyChange("Items"); }
        }
        #endregion

        #region CTor
        public FeedManagerDataModel()
        {
            UserData = new UserDataModel();

            FeedsClient.GetFeedsCompleted += new EventHandler<GetFeedsCompletedEventArgs>(FeedsClient_GetFeedsCompleted);
            FeedsClient.AddNewFeedCompleted += new EventHandler<AddNewFeedCompletedEventArgs>(FeedsClient_AddNewFeedCompleted);
            FeedsClient.UnfollowFeedCompleted += new EventHandler<UnfollowFeedCompletedEventArgs>(FeedsClient_UnfollowFeedCompleted);
            FeedsClient.GetFeedItemsCompleted += new EventHandler<GetFeedItemsCompletedEventArgs>(FeedsClient_GetFeedItemsCompleted);
        }
        #endregion

        #region OnEndAction
        void FeedsClient_AddNewFeedCompleted(object sender, AddNewFeedCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    GetAllRootFeeds();
                }
            }
        }

        void FeedsClient_UnfollowFeedCompleted(object sender, UnfollowFeedCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    GetAllRootFeeds();
                }
            }
        }

        void FeedsClient_GetFeedsCompleted(object sender, GetFeedsCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    Channels = e.Result.Value.ToList();
                }
            }
        }

        void FeedsClient_GetFeedItemsCompleted(object sender, GetFeedItemsCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    Items = e.Result.Value.ToList();
                }
            }
        }
        #endregion

        #region Action
        public void GetAllRootFeeds()
        {
            UserData.ShowConnexionModel_IFN();
            FeedsClient.GetFeedsAsync(UserData.GetConnectionString());
        }

        public void AddNewFeed(string url)
        {
            UserData.ShowConnexionModel_IFN();
            try
            {
                FeedsClient.AddNewFeedAsync(UserData.GetConnectionString(), new Uri(url));
            }
            catch (Exception)
            {
                
            }
            
        }

        public void RemoveFeed(Channel feed)
        {
            UserData.ShowConnexionModel_IFN();
            FeedsClient.UnfollowFeedAsync(UserData.GetConnectionString(), feed);
        }
        #endregion


        public void LoadFeedItems(Channel rssFeed)
        {
            UserData.ShowConnexionModel_IFN();
            FeedsClient.GetFeedItemsAsync(UserData.GetConnectionString(), rssFeed);
        }
    }
}
