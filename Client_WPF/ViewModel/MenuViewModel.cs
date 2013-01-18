using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DataModel;
using Client_WPF.View;
using System.Windows.Input;
using Client_WPF.Utils;
using System.Windows;
using System.ComponentModel;
using Common.Utils;

namespace Client_WPF.ViewModel
{
    class MenuViewModel : BindableObject
    {
        #region Properties
        private string search;
        public string Search
        {
            get { return search; }
            set
            {
                search = value;
                RaisePropertyChange("Search");
                SearchDataModel.Instance.Search = search;
            }
        }

        public ICommand CloseCommand { get; private set; }
        public ICommand Logout { get; private set; }
        public ICommand ShowConnectionDialog { get; private set; }
        public ICommand OpenAdmin { get; private set; }

        public LoginModal ConnectionModal { get; private set; }
        
        private bool isAdmin;
        public bool IsAdmin
        {
            get { return isAdmin; }
            set { isAdmin = value; RaisePropertyChange("IsAdmin"); }
        }
        

        private PropertyChangedEventHandler PropertyChangedHandler { get; set; }
        #endregion

        

        #region CTor
        public MenuViewModel()
        {
            CloseCommand = new RelayCommand((param) => Application.Current.Shutdown());
            ShowConnectionDialog = new RelayCommand((param) =>
            {
                ShowConnectionModel_IFN();
            });
            Logout = new RelayCommand((param) => UserDataModel.Instance.Logout());
            PropertyChangedHandler = new System.ComponentModel.PropertyChangedEventHandler(UserData_PropertyChanged);
            OpenAdmin = new RelayCommand((param) => 
                {
                    new AdminWindow().Show();
                });
            UserDataModel.Instance.PropertyChanged += PropertyChangedHandler;
            
            search = SearchDataModel.Instance.Search;
            IsAdmin = UserDataModel.Instance.IsConnected;
            IsAdmin = false;
        }

        ~MenuViewModel()
        {
            UserDataModel.Instance.PropertyChanged -= PropertyChangedHandler;
        }

        void UserData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                
                if (!(sender as UserDataModel).IsConnected)
                {
                    if (ConnectionModal == null)
                    {
                        ConnectionModal = new LoginModal();
                        ConnectionModal.ShowDialog();
                        ConnectionModal = null;
                    }
                    FeedManagerDataModel.Instance.GetAllRootFeeds();
                    IsAdmin = false;
                }
                else
                {
                    if (ConnectionModal != null)
                        ConnectionModal.Close();
                    ConnectionModal = null;
                    if (UserDataModel.Instance.User != null)
                        IsAdmin = UserDataModel.Instance.User.IsSuperUser;
                }
            }
            IsAdmin = true;
        }
        #endregion

        #region private methods
        void ShowConnectionModel_IFN()
        {
            if (!UserDataModel.Instance.IsConnected)
            {
                if (ConnectionModal == null)
                {
                    ConnectionModal = new LoginModal();
                    ConnectionModal.ShowDialog();
                    ConnectionModal = null;
                }
            }
            
        }
        #endregion

    }
}
