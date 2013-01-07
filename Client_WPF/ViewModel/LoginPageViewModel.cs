using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Client_WPF.Utils;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Common.DataModel;

namespace Client_WPF.ViewModel
{
    public class LoginPageViewModel : BindableObject
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

        private bool _register;
        public bool Register
        {
            get { return _register; }
            set
            {
                _register = value;
                RaisePropertyChange("Register");
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
        #endregion

        #region CTor
        public LoginPageViewModel()
        {
            UserData = new UserDataModel();
            UserData.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(UserData_PropertyChanged);
            Login = new RelayCommand((param) => LoginBody(param as string[]));
            _logued = false;
        }

        void UserData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                Logued = (sender as UserDataModel).IsConnected;
                RaisePropertyChange("Logued");
            }
        }
        #endregion

        #region BodyCommand
        private void LoginBody(string[] param)
        {
            if (Register)
                UserData.Register(Username, Password);
            else
                UserData.Login(Username, Password);
        }
        #endregion

    }
}
