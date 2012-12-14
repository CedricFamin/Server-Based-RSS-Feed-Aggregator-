using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client_WPF.RSSService;
using System.ServiceModel;
using Client_WPF.View;

namespace Client_WPF.DataModel
{
    class UserDataModel
    {
        #region Common
        static private string ConnectionString = null;
        static private AccountClient accountClient = null;
        private static User user { get; set; }

        static public User User
        {
            get { return user; }
            private set { user = value; }
        }

        static private AccountClient AccountClient
        {
            get
            {
                if (accountClient == null)
                    accountClient = new AccountClient();
                if (accountClient.State == CommunicationState.Closed)
                    accountClient.Open();
                return accountClient;
            }
        }
        #endregion

        public WebResult.ErrorCodeList Error { get; private set; }
        private static LoginModal LoginView = new LoginModal();

        #region CTor/DTor
        public UserDataModel()
        {
            AccountClient.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(OnEndLogin);
            AccountClient.RegisterCompleted += new EventHandler<RegisterCompletedEventArgs>(OnEndRegister);

            
        }
        #endregion

        #region Methods
        public bool IsConnected()
        {
            return ConnectionString != null;
        }

        public void ShowConnexionModel_IFN()
        {
            if (!IsConnected())
            {
                LoginView.ShowDialog();
            }
        }
        #endregion

        #region Action
        public void Login(string username, string password)
        {
            AccountClient.LoginAsync(username, password);
        }

        public void Register(string username, string password)
        {
            AccountClient.RegisterAsync(username, username, password);
        }

        public void Logout(string username, string password)
        {
            AccountClient.LogoutAsync(ConnectionString);
            ConnectionString = null;
        }

        public void Update()
        {
            ShowConnexionModel_IFN();
            AccountClient.Update(ConnectionString, User);
        }
        #endregion

        #region ResultAsync
        private void OnEndLogin(object sender, LoginCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    ConnectionString = args.Result.Value.Item1.session_key;
                    User = args.Result.Value.Item2;
                    LoginView.Close();
                }
                Error = args.Result.ErrorCode;
            }
        }

        private void OnEndRegister(object sender, RegisterCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                Error = args.Result.ErrorCode;
            }
        }
        #endregion
    }
}
