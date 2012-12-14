using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Utils;
using Server.EntityFramwork;
using Server.Services;

namespace Server.Interface
{
    interface IAccount
    {
        WebResult Register(string username, string email, string password);
        WebResult<Tuple<Session, AccountData>> Login(string username, string password);
        WebResult Logout(string session_key);
        WebResult Update(string session_key, AccountData updateUser);
        
        WebResult Delete(string session_key, int id);
        WebResult<List<AccountData>> UserList(string session_key);
    }
}
