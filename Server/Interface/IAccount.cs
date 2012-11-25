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
        WebResult<User> Login(string username, string password);
        WebResult Update(User updateUser);
        WebResult Delete(int id);
        WebResult<List<User>> UserList();
    }
}
