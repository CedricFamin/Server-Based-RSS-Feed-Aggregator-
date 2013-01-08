using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Utils;
using Common.DataModel;
using System.Windows.Input;
using Common.FeedService;

namespace Client_Outlook.ViewModel
{
    public class RibbonViewModel : BindableObject
    {
        #region DataModel
        private UserDataModel UserData
        {
            get {
                return UserDataModel.Instance;
            }
        }

        private FeedManagerDataModel _feedManager = null;
        private FeedManagerDataModel FeedManager
        {
            get
            {
                if (_feedManager == null)
                    _feedManager = new FeedManagerDataModel();
                return _feedManager;
            }
        }
        #endregion

        #region properties
        public ICommand Connect {get; private set; }
        public ICommand Register { get; private set; }
        public ICommand RefreshFeeds { get; private set; }
        public ICommand AddFeed { get; private set; }

        private bool _isConnected;

        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; RaisePropertyChange("IsConnected"); }
        }
        #endregion

        #region CTor
        public RibbonViewModel()
        {
            Connect     = new RelayCommand((param) => ConnectBody(param as string[]));
            Register    = new RelayCommand((param) => RegisterBody(param as string[]));
            AddFeed     = new RelayCommand((param) => AddFeedBody(param as string));
            RefreshFeeds= new RelayCommand((param) => RefreshFeedsBody());

            UserData.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UserData_PropertyChanged);
            IsConnected = UserData.IsConnected;
        }

        void UserData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
                IsConnected = UserData.IsConnected;
        }
        #endregion



        private void ConnectBody(string[] p)
        {
            UserData.Login(p[0], p[1]);
        }

        private void RegisterBody(string[] p)
        {
            UserData.Register(p[0], p[1]);
        }

        private void AddFeedBody(string url)
        {
            FeedManager.AddNewFeed(url);
        }

        private void RefreshFeedsBody()
        {
            FeedManagerViewModel.Instance.GetAllRootFeeds();
        }
    }
}
