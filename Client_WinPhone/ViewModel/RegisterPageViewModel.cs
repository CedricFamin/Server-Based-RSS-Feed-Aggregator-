using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Common.Utils;
using Common.DataModel;
using System.ComponentModel;
using System.Diagnostics;

namespace Client_WinPhone.ViewModel
{
    public class RegisterPageViewModel : BindableObject
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

        private string _passwordConfirm;
        public string PasswordConfirm
        {
            get { return _passwordConfirm; }
            set
            {
                _passwordConfirm = value;
                RaisePropertyChange("PasswordConfirm");
            }
        }

        private bool _registerMessage;
        public bool RegisterMessage
        {
            get { return _registerMessage; }
        }

        private PropertyChangedEventHandler PropertyChangedHandler { get; set; }
        #endregion

        #region CTor
        public RegisterPageViewModel()
        {
            UserData = UserDataModel.Instance;
            PropertyChangedHandler = new System.ComponentModel.PropertyChangedEventHandler(UserData_PropertyChanged);
            UserDataModel.Instance.PropertyChanged += PropertyChangedHandler;
            Login = new RelayCommand((param) => LoginBody(param as string[]));
        }

        ~RegisterPageViewModel()
        {
            UserDataModel.Instance.PropertyChanged -= PropertyChangedHandler;
        }

        void UserData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }
        #endregion

        #region BodyCommand
        private void LoginBody(string[] param)
        {
            Debug.WriteLine("Logging : " + Username + " - " + Password + " - " + PasswordConfirm);
            if (Password == PasswordConfirm)
                UserData.Register(Username, Password);
        }
        #endregion
    }
}
