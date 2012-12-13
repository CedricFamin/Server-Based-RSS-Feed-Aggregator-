using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Client_WPF.Utils;
using Client_WPF.RSSService;
using System.Net;
using System.ServiceModel.Channels;
using System.ServiceModel;
using Client_WPF.DataModel;

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

        private WebResult.ErrorCodeList _error;
        public WebResult.ErrorCodeList ErrorCode
        {
            get { return _error; }
            set
            {
                _error = value;
                RaisePropertyChange("ErrorCode");
            }
        }
        #endregion

        #region CTor
        public LoginPageViewModel()
        {
            UserData = new UserDataModel();
            Login = new RelayCommand((param) => LoginBody(param as string[]));
            _error = 0;
            _logued = false;
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
