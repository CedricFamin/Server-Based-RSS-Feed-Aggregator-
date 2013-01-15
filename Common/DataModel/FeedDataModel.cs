using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.FeedService;
using System.ServiceModel;
using Common.Utils;

namespace Common.DataModel
{
    public class FeedManagerDataModel : BindableObject
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

        private List<Channel> channels = new List<Channel>();
        public List<Channel> Channels 
        {
            get { return channels; }
            private set { channels = value; RaisePropertyChange("Channels"); }
        }

        private List<Channel> allChannels = new List<Channel>();
        public List<Channel> AllChannels
        {
            get { return allChannels; }
            private set { allChannels = value; RaisePropertyChange("AllChannels"); }
        }

        private List<Item> item = new List<Item>();
        public List<Item> Items
        {
            get { return item; }
            private set { item = value; RaisePropertyChange("Items"); }
        }
        #endregion

        #region CTor
        private static FeedManagerDataModel instance = new FeedManagerDataModel();
        public static FeedManagerDataModel Instance { get { return instance; } }
        private FeedManagerDataModel()
        {
            try
            {
                UserData = UserDataModel.Instance;

                FeedsClient.GetFeedsCompleted += new EventHandler<GetFeedsCompletedEventArgs>(FeedsClient_GetFeedsCompleted);
                FeedsClient.AddNewFeedCompleted += new EventHandler<AddNewFeedCompletedEventArgs>(FeedsClient_AddNewFeedCompleted);
                FeedsClient.UnfollowFeedCompleted += new EventHandler<UnfollowFeedCompletedEventArgs>(FeedsClient_UnfollowFeedCompleted);
                FeedsClient.GetFeedItemsCompleted += new EventHandler<GetFeedItemsCompletedEventArgs>(FeedsClient_GetFeedItemsCompleted);
                feedsClient.GetAllFeedsCompleted += new EventHandler<GetAllFeedsCompletedEventArgs>(feedsClient_GetAllFeedsCompleted);
                FeedsClient.RefreshFeedCompleted += new EventHandler<RefreshFeedCompletedEventArgs>(FeedsClient_RefreshFeedCompleted);

                if (UserData.IsConnected)
                    FeedsClient.GetAllFeeds();
            }
            catch (Exception)
            {
                
                // HACK BIZARRE BUG DE BLEND
            }
            
        }
        #endregion

        #region OnEndAction
        void FeedsClient_RefreshFeedCompleted(object sender, RefreshFeedCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    GetAllRootFeeds();
                }
            }
        }

        void feedsClient_GetAllFeedsCompleted(object sender, GetAllFeedsCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    AllChannels = e.Result.Value.ToList();
                }
            }
        }

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
            FeedsClient.GetAllFeedsAsync();
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

        public void RefreshFeed(Channel feed)
        {
            FeedsClient.RefreshFeed(feed);
        }
        #endregion


        public void LoadFeedItems(Channel rssFeed)
        {
            UserData.ShowConnexionModel_IFN();
            FeedsClient.GetFeedItemsAsync(UserData.GetConnectionString(), rssFeed);
        }
    }

    public class FeedDetailsDataModel : BindableObject
    {

        #region Common
        private FeedsServiceClient feedsClient = null;
        private FeedsServiceClient FeedsClient
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
        public Channel RootChannel { get; set; }

        private List<Item> items;

        public List<Item> Items
        {
            get { return items; }
            set { items = value; RaisePropertyChange("Items"); }
        }
        #endregion

        public FeedDetailsDataModel()
        {
            UserData = UserDataModel.Instance;
            UserData.ShowConnexionModel_IFN();

            FeedsClient.ReadItemCompleted += new EventHandler<ReadItemCompletedEventArgs>(FeedsClient_ReadItemCompleted);
        }

        public FeedDetailsDataModel(Channel rootChan)
        {
            if (rootChan == null)
                throw new NullReferenceException();

            UserData = UserDataModel.Instance;
            UserData.ShowConnexionModel_IFN();

            RootChannel = rootChan;
            FeedsClient.GetFeedItemsCompleted += new EventHandler<GetFeedItemsCompletedEventArgs>(FeedsClient_GetFeedItemsCompleted);
            FeedsClient.GetFeedItemsAsync(UserData.GetConnectionString(), rootChan);
            FeedsClient.ReadItemCompleted += new EventHandler<ReadItemCompletedEventArgs>(FeedsClient_ReadItemCompleted);
        }

        #region db result
        void FeedsClient_ReadItemCompleted(object sender, ReadItemCompletedEventArgs e)
        {
            
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

        #region action
        public void ReadItem(Item item)
        {
            UserData.ShowConnexionModel_IFN();
            FeedsClient.ReadItemAsync(UserData.GetConnectionString(), item);
            item.Read = true;
        }
        #endregion
    }
}
