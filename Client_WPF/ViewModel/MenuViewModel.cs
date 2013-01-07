using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DataModel;
using Client_WPF.View;
using System.Windows.Input;
using Client_WPF.Utils;
using System.Windows;

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

            }
        }

        public ICommand CloseCommand { get; private set; }
        public ICommand ShowConnectionDialog { get; private set; }

        private UserDataModel UserData { get;set; }

        #endregion

        public LoginModal ConnectionModal { get; private set; }

        #region CTor
        public MenuViewModel()
        {
            UserData = new UserDataModel();
            CloseCommand = new RelayCommand((param) => Application.Current.Shutdown());
            ShowConnectionDialog = new RelayCommand((param) => ShowConnectionModel_IFN());
            UserData.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UserData_PropertyChanged);
            
            search = "Search Feeds ...";
        }

        void UserData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                if (!(sender as UserDataModel).IsConnected)
                {
                    ConnectionModal = new LoginModal();
                    ConnectionModal.ShowDialog();
                }
                else
                {
                    ConnectionModal.Close();
                    ConnectionModal = null;
                }
            }
        }
        #endregion

        #region private methods
        void ShowConnectionModel_IFN()
        {
            ConnectionModal = new LoginModal();
            ConnectionModal.ShowDialog();
        }
        #endregion

    }
}
