using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.EntityFramwork;

namespace Server.Utils
{
    /// <summary>
    /// Petite class outils permettant de gérer les session
    /// A utiliser avec de l'aggregation
    /// </summary>
    public sealed class SessionWrapper
    {
        #region Database
        ServerDataContext db = new ServerDataContext();
        #endregion

        public Session GetSession(string sessionKey)
        {
            try
            {
                return (from s in db.Sessions where s.session_key == sessionKey select s).Single();
            }
            catch
            {
                return null;
            }
        }

        public User GetUser(Session session)
        {
            return (from u in db.Users where u.id == session.id_user select u).Single();
        }

        public Session CreateSession(User user)
        {
            Session session = new Session()
            {
                id_user = user.id,
                session_key = Guid.NewGuid().ToString(),
                expire = DateTime.Now
            };
            db.Sessions.InsertOnSubmit(session);
            db.SubmitChanges();
            return GetSession(session.session_key);
        }

        public void DeleteSession(string session_key)
        {
            Session s = GetSession(session_key);
            if (s != null)
            {
                db.Sessions.DeleteOnSubmit(s);
                db.SubmitChanges();
            }
        }

        public User GetUser(string connectionKey)
        {
            Session session = GetSession(connectionKey);
            if (session == null)
                return null;
            return GetUser(session);
        }
    }
}