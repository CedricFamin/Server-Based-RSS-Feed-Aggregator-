using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Client_WPF.Utils;
using Client_WPF.RSSService;
using System.Net;
using Client_WPF.ServiceReference;
using System.ServiceModel.Channels;
using System.ServiceModel;

namespace Client_WPF.ViewModel
{
    public class LoginPageViewModel : BindableObject
    {
        private User _user;
        CookieContainer cookie;

        public User User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChange("User");
            }
        }


        #region properties
        public ICommand Login { get; private set; }
        private AccountClient accountService { get; set; }
        private AuthenticationServiceClient authService { get; set; }

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

        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                RaisePropertyChange("Email");
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
            _username = "toto";
            Login = new RelayCommand((param) => LoginBody(param as string[]));

            authService = new AuthenticationServiceClient();
            authService.Endpoint.Behaviors.Add(new CookieBehavior(""));

            accountService = new AccountClient();
            
            accountService.LoginCompleted += new EventHandler<LoginCompletedEventArgs>(OnEndLogin);
            accountService.RegisterCompleted += new EventHandler<RegisterCompletedEventArgs>(OnEndRegister);
            _error = 0;
            _logued = false;
        }
        #endregion

        string sharedCookie;
        #region ResultAsync
        private void OnEndLogin(object sender, LoginCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    // Extract the cookie embedded in the received web service response
                    // and stores it locally
                    HttpResponseMessageProperty response = (HttpResponseMessageProperty)
                    OperationContext.Current.IncomingMessageProperties[
                        HttpResponseMessageProperty.Name];
                    sharedCookie = response.Headers["Set-Cookie"];

                    Logued = true;
                    User = args.Result.Value;
                }
                ErrorCode = args.Result.ErrorCode;
            }
        }

        private void OnEndRegister(object sender, RegisterCompletedEventArgs args)
        {
            if (args.Error == null)
            {
                if (args.Result.ErrorCode == WebResult.ErrorCodeList.SUCCESS)
                {
                    accountService.LoginAsync(Username, Password);
                }
                ErrorCode = args.Result.ErrorCode;
            }
        }
        #endregion

        #region BodyCommand
        private void LoginBody(string[] param)
        {
            //accountService.RegisterAsync("test", "test", "test");
            if (Register)
                accountService.RegisterAsync(Username, Email, Password);
            else
                accountService.LoginAsync(Username, Password);
        }
        #endregion
    }
}
