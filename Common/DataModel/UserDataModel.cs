using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.RSSService;
using System.ServiceModel;
using Common.Utils;
using System.IO;
using System.Configuration;

namespace Common.DataModel
{
    public class UserDataModel : BindableObject
    {
        #region Static
        static string[] GetUserSettings()
        {
            string filename = "Applications.settings";
            string applicationDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string rssFolder = System.IO.Path.Combine(applicationDataFolder, "RssFeedAggregator");
            string fileAbsolutePath = System.IO.Path.Combine(rssFolder, filename);

            string[] paths = {fileAbsolutePath, filename, rssFolder};
            return paths;
        }

        static public void SaveConnectionString()
        {
            if (Instance == null)
                return;
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);

            config.AppSettings.Settings.Remove("ConnectionString");
            config.AppSettings.Settings.Add("ConnectionString", Instance.ConnectionString);

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        static private string LoadConnectionString()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            if (config.AppSettings.Settings["ConnectionString"] != null)
                return config.AppSettings.Settings["ConnectionString"].Value;
            return "";
        }

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
                if (_connectionString != null)
                    SaveConnectionString();
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
                    accountClient.Open();
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
                if (ConnectionString == null)
                {
                    _connectionString = LoadConnectionString();
                    AccountClient.IsConnectedAsync(ConnectionString);
                }
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
            AccountClient.Update(ConnectionString, User);
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
                    ConnectionString = args.Result.Value.Item1;
                    IsConnected = true;    
                    User = args.Result.Value.Item2;
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
