using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Utils;
using Common.RSSService;
using Common.FeedService;
using System.ComponentModel;
using System.ServiceModel;

namespace Common.DataModel
{
    public class AdminDataModel : BindableObject
    {
        #region Singleton
        static private AdminDataModel _instance = new AdminDataModel();
        static public AdminDataModel Instance
        {
            get { return _instance; }
            private set { _instance = value; }
        }
        #endregion

        #region 
        private AccountClient accountClient = null;
        private AccountClient AccountClient
        {
            get{
                if (accountClient == null)
                    accountClient = new AccountClient("AccountService");
                if (accountClient.State == CommunicationState.Closed)
                    accountClient.Open();
                return accountClient;
            }
        }
        
        #endregion

        #region Callback
        PropertyChangedEventHandler propertyChangeFeedManager;
        PropertyChangedEventHandler propertyChangeUserDataModel;
        #endregion

        #region CTor/DTor
        private AdminDataModel()
        {
            try
            {
                // callback init
                propertyChangeFeedManager = new PropertyChangedEventHandler(FeedManager_PropertyChanged);
                propertyChangeUserDataModel = new PropertyChangedEventHandler(User_PropertyChanged);

                // binding
                FeedManagerDataModel.Instance.PropertyChanged += propertyChangeFeedManager;
                UserDataModel.Instance.PropertyChanged += propertyChangeUserDataModel;

                AccountClient.UpdateCompleted += new EventHandler<UpdateCompletedEventArgs>(AccountClient_UpdateCompleted);
                AccountClient.UserListCompleted += new EventHandler<UserListCompletedEventArgs>(AccountClient_UserListCompleted);
                AccountClient.DeleteCompleted += new EventHandler<DeleteCompletedEventArgs>(AccountClient_DeleteCompleted);
                RefreshAllUser();
                Channels = FeedManagerDataModel.Instance.AllChannels;
            }
            catch (Exception)
            {
            }
        }

        ~AdminDataModel()
        {
            FeedManagerDataModel.Instance.PropertyChanged -= propertyChangeFeedManager;
            UserDataModel.Instance.PropertyChanged -= propertyChangeUserDataModel;
        }
        #endregion

        #region PPTies
        private List<AccountData> users = new List<AccountData>();
        public List<AccountData> Users
        {
            get { return users; }
            private set { users = value; RaisePropertyChange("Users");  }
        }

        private List<Channel> channels;
        public List<Channel> Channels
        {
            get { return channels; }
            set { channels = value; RaisePropertyChange("Channels");  }
        }

        private FeedDetailsDataModel feedDetail;
        public FeedDetailsDataModel FeedDetail
        {
            get { return feedDetail; }
            set { feedDetail = value; RaisePropertyChange("FeedDetail"); }
        }
        #endregion

        #region Event properties change
        void FeedManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "AllChannels":
                    Channels = FeedManagerDataModel.Instance.AllChannels;
                    break;
            }
        }

        void User_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                if (UserDataModel.Instance.IsConnected)
                    RefreshAllUser();
            }
        }
        #endregion

        #region Action
        public void UpdateUser(RSSService.AccountData account)
        {
            if (UserDataModel.Instance.IsConnected)
            {
                AccountClient.UpdateAsync(UserDataModel.Instance.GetConnectionString(), account);
            }
        }

        public void RefreshAllUser()
        {
            if (UserDataModel.Instance.IsConnected)
            {
                AccountClient.UserListAsync("");
            }
        }

        public void GetFeedDetails(Channel chan)
        {
            if (chan != null)
                FeedDetail = new FeedDetailsDataModel(chan);
            else
                FeedDetail = null;
        }

        public void DeleteUser(AccountData user)
        {
            AccountClient.DeleteAsync(UserDataModel.Instance.GetConnectionString(), user.Id);
        }
        #endregion

        #region CallbackAsyncComplete
        void AccountClient_UpdateCompleted(object sender, UpdateCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == Common.RSSService.WebResult.ErrorCodeList.SUCCESS)
                {
                    RefreshAllUser();
                }
            }
        }
        void AccountClient_UserListCompleted(object sender, UserListCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == Common.RSSService.WebResult.ErrorCodeList.SUCCESS)
                {
                    Users = e.Result.Value.ToList();
                }
            }
        }

        void AccountClient_DeleteCompleted(object sender, DeleteCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.ErrorCode == Common.RSSService.WebResult.ErrorCodeList.SUCCESS)
                {
                    RefreshAllUser();
                }
            }
        }
        #endregion
    }
}
