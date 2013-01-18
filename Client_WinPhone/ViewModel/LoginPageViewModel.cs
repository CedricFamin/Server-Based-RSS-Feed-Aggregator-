using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Common.DataModel;
using System.ComponentModel;
using Common.Utils;
using System.Windows;
using System.Windows.Navigation;
using System.Diagnostics;
using Microsoft.Phone.Controls;

namespace Client_WinPhone.ViewModel
{
    public class LogInPageViewModel : BindableObject
    {

        #region properties
        public ICommand Login { get; private set; }
        private UserDataModel UserData { get; set; }

        private string _username;
        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                RaisePropertyChange("Username");
            }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                RaisePropertyChange("Password");
            }
        }

        private bool _logued;
        public bool Logued
        {
            get { return _logued; }
            set
            {
                _logued = value;
                RaisePropertyChange("Logued");
            }
        }

        private PropertyChangedEventHandler PropertyChangedHandler { get; set; }
        #endregion

        #region CTor
        public LogInPageViewModel()
        {
            UserData = UserDataModel.Instance;
            PropertyChangedHandler = new System.ComponentModel.PropertyChangedEventHandler(UserData_PropertyChanged);
            UserDataModel.Instance.PropertyChanged += PropertyChangedHandler;
            Login = new RelayCommand((param) => LoginBody(param as string[]));
            _logued = false;
        }

        ~LogInPageViewModel()
        {
            UserDataModel.Instance.PropertyChanged -= PropertyChangedHandler;
        }

        void UserData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                Logued = (sender as UserDataModel).IsConnected;
                RaisePropertyChange("Logued");

                if (Logued)
                {
                    PhoneApplicationFrame frame = (PhoneApplicationFrame)Application.Current.RootVisual;
                    bool success = frame.Navigate(new Uri("/View/Feeds.xaml", UriKind.Relative));
                }
            }
        }
        #endregion

        #region BodyCommand
        private void LoginBody(string[] param)
        {
            UserData.Login(Username, Password);
        }
        #endregion
    }
}
