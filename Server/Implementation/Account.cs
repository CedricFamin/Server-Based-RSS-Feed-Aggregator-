using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Interface;
using System.ComponentModel.Composition;
using Server.Utils;
using Server.EntityFramwork;
using System.Web.Security;
using Server.Services;

namespace Server.Implementation
{
    [Export(typeof(IAccount))]
    public class Account : IAccount
    {
        #region Database
        ServerDataContext db = new ServerDataContext();
        #endregion

        SessionWrapper _sessionWrapper = new SessionWrapper();

        #region IAccount
        public WebResult Register(string username, string email, string password)
        {
            WebResult result = new WebResult();

            var users = from u in db.Users where u.username == username || u.email == email select u;
            if (username == "" || password == "" || email == "")
                result.ErrorCode = WebResult.ErrorCodeList.INFORMATION_REQUIRED;
            else if (users.Count() > 0)
                result.ErrorCode = WebResult.ErrorCodeList.USER_ALREADY_EXIST;
            if (result.ErrorCode != WebResult.ErrorCodeList.SUCCESS)
                return result;
            User user = new User()
            {   
                username = username,
                email = email,
                password = password
            };
            db.Users.InsertOnSubmit(user);
            db.SubmitChanges();
            return result;
        }

        public WebResult<string, AccountData> Login(string username, string password)
        {
            try
            {
                var users = from u in db.Users where u.username == username && u.password == password select u;
                if (users.Count() > 1)
                    return new WebResult<string, AccountData>(WebResult.ErrorCodeList.INTERNAL_ERROR);
                if (users.Count() == 0)
                    return new WebResult<string, AccountData>(WebResult.ErrorCodeList.USER_NOT_FOUND);
                User user = (users).Single();
                Session session = _sessionWrapper.CreateSession(user);
                return new WebResult<string, AccountData>(session.session_key, new AccountData(user));
                
            }
            catch
            {
                return new WebResult<string, AccountData>(WebResult.ErrorCodeList.USER_NOT_FOUND);
            }
        }

        public WebResult Logout(string session_key)
        {
            return new WebResult();
        }

        public WebResult Update(string session_key, AccountData updateUser)
        {
            Session session = _sessionWrapper.GetSession(session_key);

            if (session == null)
                return new WebResult(WebResult.ErrorCodeList.NOT_LOGUED);

            User user = _sessionWrapper.GetUser(session);

            user.username = updateUser.Username;
            user.password = updateUser.Password;
            user.email = updateUser.Username;
            db.SubmitChanges();
            return new WebResult();
        }

        public WebResult Delete(string session_key, int id)
        {
            Session session = _sessionWrapper.GetSession(session_key);

            if (session == null) return new WebResult(WebResult.ErrorCodeList.NOT_LOGUED);
            User user = _sessionWrapper.GetUser(session);

            if (!user.superuser) return new WebResult(WebResult.ErrorCodeList.NEED_PRIVILEGE);

            try
            {
                var users = from u in db.Users where u.id == id select u;
                if (users.Count() > 1)
                    return new WebResult<Tuple<string, AccountData>>(WebResult.ErrorCodeList.INTERNAL_ERROR);
                if (users.Count() == 0)
                    return new WebResult<Tuple<string, AccountData>>(WebResult.ErrorCodeList.USER_NOT_FOUND);
                var userToDelete = (users).Single();
                db.Users.DeleteOnSubmit(userToDelete);
                db.SubmitChanges();
                return new WebResult();
            }
            catch
            {
                return new WebResult(WebResult.ErrorCodeList.USER_NOT_FOUND);
            }
        }


        public WebResult<List<AccountData>> UserList(string session_key)
        {
            List<AccountData> users = new List<AccountData>();
            foreach (var user in db.Users)
            {
                users.Add(new AccountData(user));
            }
            return new WebResult<List<AccountData>>(users);
        }

        public WebResult<bool> IsConnected(string session_key)
        {
            Session session = _sessionWrapper.GetSession(session_key);
            return new WebResult<bool>(session != null);
        }
        #endregion
    }
}