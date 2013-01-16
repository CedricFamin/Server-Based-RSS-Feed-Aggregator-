using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using Common.Utils;
using System.IO;
using System.Configuration;
using Client_WinPhone.RSSService;

namespace Common.DataModel
{
    public class UserDataModel : BindableObject
    {
        #region Static
        private static UserDataModel _instance = new UserDataModel();
        public static UserDataModel Instance { get { return _instance; } }
        #endregion
        #region Common
        private string _connectionString;
        private string ConnectionString { 
            get
            {
                return _connectionString;
            } 
            set
            {
                _connectionString = value;
            }
        }
        private AccountClient accountClient = null;
        static AccountData user { get; set; }

        public AccountData User
        {
            get { return user; }
            private set { user = value; }
        }

        private AccountClient AccountClient
        {
            get
            {
                if (accountClient == null)
                {
                    try
                    {
                        accountClient = new AccountClient();
                    }
                    catch (Exception)
                    {
                        return null;
                    }
                }
                if (accountClient.State == CommunicationState.Closed)
                    accountClient.OpenAsync();
                return accountClient;
            }
        }
        #endregion
        
        //private static LoginModal LoginView = new LoginModal();

        #region CTor/DTor
        private UserDataModel()
        {
            try
            {
                AccountClient.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(OnEndLogin);
                AccountClient.RegisterCompleted += new EventHandler<RegisterCompletedEventArgs>(OnEndRegister);
                AccountClient.IsConnectedCompleted += new EventHandler<IsConnectedCompletedEventArgs>(OnIsConnected);
                IsConnected = false;
            }
            catch (Exception)
            {
                // HACK BIZARRE BUG DE BLEND
            }
        }
        #endregion

        #region Methods
        public bool IsConnected { get; private set; }

        public void ShowConnexionModel_IFN()
        {
            if (!IsConnected)
            {
                /*
                if (!LoginView.IsActive)
                    LoginView = new LoginModal();
                LoginView.ShowDialog();
                 * */
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
            AccountClient.UpdateAsync(ConnectionString, User);
        }

        public void Logout()
        {
            AccountClient.LogoutAsync(ConnectionString);
            ConnectionString = "";
            IsConnected = false;
            RaisePropertyChange("IsConnected");
        }
        #endregion

        #region ResultAsync
        private void OnIsConnected(object sender, IsConnectedCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    IsConnected = args.Result.Value;
                    RaisePropertyChange("IsConnected");
                }
            }
        }

        private void OnEndLogin(object sender, LoginCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    ConnectionString = args.Result.Value1;
                    IsConnected = true;    
                    User = args.Result.Value2;
                    //LoginView.Close();
                }
                else
                    IsConnected = false;
                RaisePropertyChange("IsConnected");
            }
        }

        private void OnEndRegister(object sender, RegisterCompletedEventArgs args)
        {
            if (args.Error == null)
            {

            }
        }
        #endregion

        public string GetConnectionString()
        {
            return ConnectionString;
        }
    }
}
