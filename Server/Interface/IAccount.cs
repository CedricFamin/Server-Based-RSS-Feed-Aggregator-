using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Utils;
using Server.EntityFramwork;

namespace Server.Interface
{
    interface IAccount
    {
        WebResult Register(string username, string email, string password);
        WebResult<Tuple<Session, User>> Login(string username, string password);
        WebResult Logout(string session_key);
        WebResult Update(string session_key, User updateUser);
        
        WebResult Delete(string session_key, int id);
        WebResult<List<User>> UserList(string session_key);
    }
}
