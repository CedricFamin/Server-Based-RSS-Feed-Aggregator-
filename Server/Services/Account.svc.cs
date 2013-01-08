using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Activation;
using Server.Interface;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using Server.Utils;
using Server.EntityFramwork;
using System.Web.Security;
using System.Threading;
using System.Web;

namespace Server.Services
{
    [DataContract]
    public class AccountData
    {
        public AccountData()
        {
        }

        public AccountData(User user)
        {
            Id = user.id;
            Username = user.username;
            Password = user.password;
            IsSuperUser = user.superuser;
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Username{ get; set; }
        [DataMember]
        public String Password { get; set; }
        [DataMember]
        public bool IsSuperUser { get; set; }
    }

    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Account : IAccount
    {
        #region Fields
        private CompositionContainer _container;

        [Import(typeof(IAccount))]
        private IAccount _account = null;
        #endregion

        #region CTor
        public Account()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(Account).Assembly));

            _container = new CompositionContainer(catalog);
            try
            {
                this._container.ComposeParts(this);
            }
            catch (CompositionException compositionException)
            {
                Console.WriteLine(compositionException.ToString());
            }
        }
        #endregion

        #region OperationContract
        [OperationContract]
        public WebResult Register(string username, string email, string password)
        {
            return this._account.Register(username, email, password);
        }

        [OperationContract]
        public WebResult<Tuple<string, AccountData>> Login(string username, string password)
        {
            return this._account.Login(username, password);     
        }

        [OperationContract]
        public WebResult Logout(string session_key)
        {
            return this._account.Logout(session_key);
        }

        [OperationContract]
        public WebResult Update(string session_key, AccountData updateUser)
        {
            return this._account.Update(session_key, updateUser);
        }

        [OperationContract]
        public WebResult Delete(string session_key, int id)
        {
            return this._account.Delete(session_key, id);
        }

        [OperationContract]
        public WebResult<List<AccountData>> UserList(string session_key)
        {
            return this._account.UserList(session_key);
        }

        [OperationContract]
        public WebResult<bool> IsConnected(string session_key)
        {
            return this._account.IsConnected(session_key);
        }
        #endregion
    }
}
