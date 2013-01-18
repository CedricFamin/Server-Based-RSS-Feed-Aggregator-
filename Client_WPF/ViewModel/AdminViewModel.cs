using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.DataModel;
using System.ComponentModel;
using Common.RSSService;
using Common.FeedService;
using System.Windows.Input;
using Common.Utils;

namespace Client_WPF.ViewModel
{
    class AdminViewModel : BindableObject
    {
        private List<AccountData> users;
        public List<AccountData> Users
        {
            get { return users; }
            set { users = value; RaisePropertyChange("Users"); }
        }

        private List<Channel> channels;
        public List<Channel> Channels
        {
            get { return channels; }
            set { channels = value; RaisePropertyChange("Channels"); }
        }

        private FeedDetailsDataModel feedDetail;
        public FeedDetailsDataModel FeedDetail
        {
            get { return feedDetail; }
            set { feedDetail = value; RaisePropertyChange("FeedDetail"); }
        }
        public ICommand UpdateUser { get; private set; }
        public ICommand RemoveUser { get; private set; }
        public ICommand GetFeedDetails { get; private set; }
        
        #region Callback
        private PropertyChangedEventHandler PropertyChangeAdminDataModel;
        #endregion

        #region CTor/DTor
        public AdminViewModel()
        {
            PropertyChangeAdminDataModel = new PropertyChangedEventHandler(AdminDataModel_PropertyChanged);
            AdminDataModel.Instance.PropertyChanged += PropertyChangeAdminDataModel;
            Channels = AdminDataModel.Instance.Channels;
            Users = AdminDataModel.Instance.Users;

            UpdateUser = new RelayCommand((param) => AdminDataModel.Instance.UpdateUser(param as AccountData));
            GetFeedDetails = new RelayCommand((param) => AdminDataModel.Instance.GetFeedDetails(param as Channel));
            RemoveUser = new RelayCommand((param) => AdminDataModel.Instance.DeleteUser(param as AccountData));
        }

        ~AdminViewModel()
        {
            AdminDataModel.Instance.PropertyChanged -= PropertyChangeAdminDataModel;

            UpdateUser = new RelayCommand((param) => AdminDataModel.Instance.UpdateUser(param as Common.RSSService.AccountData));
        }
        #endregion

        #region Property change
        void AdminDataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Users":
                    Users = AdminDataModel.Instance.Users;
                    break;
                case "Channels":
                    Channels = AdminDataModel.Instance.Channels;
                    break;
                case "FeedDetail":
                    FeedDetail = AdminDataModel.Instance.FeedDetail;
                    break;
            }
        }
        #endregion
    }
}
