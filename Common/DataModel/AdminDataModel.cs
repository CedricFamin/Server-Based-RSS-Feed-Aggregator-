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
        private AdminDataModel _instance = new AdminDataModel();

        public AdminDataModel Instance
        {
            get { return _instance = new AdminDataModel(); }
            private set { _instance = value; }
        }
        #endregion

        #region 
        private AccountClient accountClient = new AccountClient();
        private AccountClient AccountClient
        {
            get{
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
            // callback init
            propertyChangeFeedManager = new PropertyChangedEventHandler(FeedManager_PropertyChanged);
            propertyChangeUserDataModel = new PropertyChangedEventHandler(User_PropertyChanged);

            // binding
            FeedManagerDataModel.Instance.PropertyChanged += propertyChangeFeedManager;
            UserDataModel.Instance.PropertyChanged += propertyChangeUserDataModel;

            AccountClient.UpdateCompleted += new EventHandler<UpdateCompletedEventArgs>(AccountClient_UpdateCompleted);
            AccountClient.UserListCompleted += new EventHandler<UserListCompletedEventArgs>(AccountClient_UserListCompleted);
        }

        ~AdminDataModel()
        {
            FeedManagerDataModel.Instance.PropertyChanged -= propertyChangeFeedManager;
            UserDataModel.Instance.PropertyChanged -= propertyChangeUserDataModel;
        }
        #endregion

        #region PPTies
        private List<AccountData> users;
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
        #endregion

        #region Event properties change
        void FeedManager_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {

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
        void UpdateUser(AccountData account)
        {
            if (UserDataModel.Instance.IsConnected)
            {
                AccountClient.UpdateAsync(UserDataModel.Instance.GetConnectionString(), account);
            }
        }

        void RefreshAllUser()
        {
            if (UserDataModel.Instance.IsConnected)
            {
                AccountClient.UserListAsync("");
            }
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
        #endregion
    }
}
