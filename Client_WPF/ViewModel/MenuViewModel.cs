using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client_WPF.DataModel;
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

        #region CTor
        public MenuViewModel()
        {
            UserData = new UserDataModel();
            CloseCommand = new RelayCommand((param) => Application.Current.Shutdown());
            ShowConnectionDialog = new RelayCommand((param) => ShowConnectionModel_IFN());

            search = "Search Feeds ...";
        }
        #endregion

        #region private methods
        void ShowConnectionModel_IFN()
        {
            UserData.ShowConnexionModel_IFN();
        }
        #endregion

    }
}
