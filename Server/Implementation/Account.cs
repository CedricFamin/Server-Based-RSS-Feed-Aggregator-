using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Interface;
using System.ComponentModel.Composition;
using Server.Utils;
using Server.EntityFramwork;
using System.Web.Security;

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

        public WebResult<Tuple<Session, User>> Login(string username, string password)
        {
            try
            {
                
                User user = (from u in db.Users where u.username == username && u.password == password select u).Single();
                Session session = _sessionWrapper.CreateSession(user);
                return new WebResult<Tuple<Session, User>>(new Tuple<Session, User>(session, user));
                
            }
            catch
            {
                return new WebResult<Tuple<Session, User>>(WebResult.ErrorCodeList.USER_NOT_FOUND);
            }
        }

        public WebResult Logout(string session_key)
        {
            return new WebResult();
        }

        public WebResult Update(string session_key, User updateUser)
        {
            Session session = _sessionWrapper.GetSession(session_key);

            if (session == null)
                return new WebResult(WebResult.ErrorCodeList.NOT_LOGUED);

            User user = _sessionWrapper.GetUser(session);

            user.username = updateUser.username;
            user.password = updateUser.password;
            user.email = updateUser.email;
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
                var userToDelete = (from u in db.Users where u.id == id select u).Single();
                db.Users.DeleteOnSubmit(userToDelete);
                db.SubmitChanges();
                return new WebResult();
            }
            catch
            {
                return new WebResult(WebResult.ErrorCodeList.USER_NOT_FOUND);
            }
        }


        public WebResult<List<User>> UserList(string session_key)
        {
            return new WebResult<List<User>>(db.Users.ToList());
        }
        #endregion
    }
}