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

        private List<RssFeed> rootFeeds = null;
        public List<RssFeed> RootFeeds 
        { 
            get { return rootFeeds; }
            private set { rootFeeds = value; RaisePropertyChange("RootFeeds"); }
        }
        #endregion

        #region CTor
        public FeedManagerDataModel()
        {
            UserData = new UserDataModel();
            RootFeeds = new List<RssFeed>();

            FeedsClient.GetFeedsCompleted += new EventHandler<GetFeedsCompletedEventArgs>(FeedsClient_GetFeedsCompleted);
            FeedsClient.AddNewFeedCompleted += new EventHandler<AddNewFeedCompletedEventArgs>(FeedsClient_AddNewFeedCompleted);
            FeedsClient.UnfollowFeedCompleted += new EventHandler<UnfollowFeedCompletedEventArgs>(FeedsClient_UnfollowFeedCompleted);
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
                    RootFeeds = e.Result.Value.ToList();
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
            FeedsClient.AddNewFeedAsync(UserData.GetConnectionString(), new Uri(url));
        }

        public void RemoveFeed(RssFeed feed)
        {
            UserData.ShowConnexionModel_IFN();
            FeedsClient.UnfollowFeedAsync(UserData.GetConnectionString(), feed);
        }
        #endregion

    }
}
