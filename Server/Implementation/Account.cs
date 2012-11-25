using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.Interface;
using System.ComponentModel.Composition;
using Server.Utils;
using Server.EntityFramwork;

namespace Server.Implementation
{
    [Export(typeof(IAccount))]
    public class Account : IAccount
    {
        ServerDataContext db = new ServerDataContext();

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

        public WebResult<User> Login(string username, string password)
        {
            try
            {
                
                return new WebResult<User>((from u in db.Users where u.username == username && u.password == password select u).Single()); ;
            }
            catch
            {
                return new WebResult<User>(WebResult.ErrorCodeList.USER_NOT_FOUND);
            }
        }

        public WebResult Update(User updateUser)
        {
            try
            {
                var user = (from u in db.Users where u.id == updateUser.id select u).Single();
                user.username = updateUser.username;
                user.password = updateUser.password;
                user.email = updateUser.email;
                db.SubmitChanges();
                return new WebResult();
            }
            catch
            {
                return new WebResult(WebResult.ErrorCodeList.USER_NOT_FOUND);
            }
        }

        public WebResult Delete(int id)
        {
            try
            {
                var user = (from u in db.Users where u.id == id select u).Single();
                db.Users.DeleteOnSubmit(user);
                db.SubmitChanges();
                return new WebResult();
            }
            catch
            {
                return new WebResult(WebResult.ErrorCodeList.USER_NOT_FOUND);
            }
        }


        public WebResult<List<User>> UserList()
        {
            return new WebResult<List<User>>(db.Users.ToList());
        }
    }
}