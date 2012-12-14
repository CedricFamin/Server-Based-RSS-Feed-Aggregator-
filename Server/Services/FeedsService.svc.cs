using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Server.Interface;
using System.Xml;
using System.ServiceModel.Syndication;
using System.ServiceModel.Activation;
using Server.Utils;
using Server.EntityFramwork;

namespace Server.Services
{
    [DataContract]
    public class RssFeed
    {
        public RssFeed()
        {
        }

        public RssFeed(Feed dbFeed)
        {
            Id = dbFeed.id;
            Title = dbFeed.title;
            Description = dbFeed.description;
            Url = dbFeed.url;
            Date = dbFeed.date;
            Link = dbFeed.link;
        }

        public Feed ToDbFeed()
        {
            ServerDataContext db = new ServerDataContext();
            return (from f in db.Feeds where f.id == Id select f).SingleOrDefault();
        }

        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdParent { get; set; }
        [DataMember]
        public String Title { get; set; }
        [DataMember]
        public String Description { get; set; }
        [DataMember]
        public String Url { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Link { get; set; }
    }

    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FeedsService : IFeeds
    {
        #region Database
        ServerDataContext db = new ServerDataContext();
        #endregion

        SessionWrapper _sessionWrapper = new SessionWrapper();

        #region Utils
        RssFeed CreateFeedIFN(Uri uri)
        {
            Feed dbFeed = (from f in db.Feeds where f.url == uri.ToString() select f).SingleOrDefault();
            if (dbFeed == null)
            {
                SyndicationFeed feed = null;
                try
                {
                    XmlReader reader = XmlReader.Create(uri.ToString());
                    feed = SyndicationFeed.Load(reader);
                    if (feed == null)
                        return null;
                }
                catch (Exception)
                {
                    return null;
                }
                dbFeed = CreateDbFeed(feed, uri);
            }

            return new RssFeed(dbFeed);

        }

        private Feed CreateDbFeed(SyndicationFeed feed, Uri uri)
        {
            Feed dbFeed = new Feed()
            {
                title = feed.Title.Text,
                date = feed.LastUpdatedTime.DateTime,
                description = feed.Description.Text,
                url = uri.ToString(),
                link = feed.Links[0].ToString()
            };

            db.Feeds.InsertOnSubmit(dbFeed);
            db.SubmitChanges();
            return dbFeed;
        }
        #endregion

        [OperationContract]
        public WebResult<RssFeed> AddNewFeed(string connectionKey, Uri uri)
        {
            User user = _sessionWrapper.GetUser(connectionKey);

            if (user == null)
                return new WebResult<RssFeed>(WebResult.ErrorCodeList.NOT_LOGUED);

            RssFeed feed = CreateFeedIFN(uri);

            if (feed == null)
                return new WebResult<RssFeed>(WebResult.ErrorCodeList.CANNOT_CREATE_FEED);
            FeedByUser feedXUser = (from uf in db.FeedByUsers where uf.id_user == user.id && uf.id_feed == feed.Id select uf).SingleOrDefault();
            if (feedXUser == null)
            {
                feedXUser = new FeedByUser()
                {
                    id_feed = feed.Id,
                    id_user = user.id
                };
                db.FeedByUsers.InsertOnSubmit(feedXUser);
                db.SubmitChanges();
            }
            return new WebResult<RssFeed>(feed);
        }

        [OperationContract]
        public WebResult<List<RssFeed>> GetFeeds(string connectionKey)
        {
            User user = _sessionWrapper.GetUser(connectionKey);

            List<RssFeed> feeds = new List<RssFeed>();

            if (user == null)
                return new WebResult<List<RssFeed>>(WebResult.ErrorCodeList.NOT_LOGUED);

            var dbFeeds = from uf in db.FeedByUsers where uf.User == user select uf.Feed;
            foreach (Feed dbFeed in dbFeeds)
            {
                feeds.Add(new RssFeed(dbFeed));
            }
            return new WebResult<List<RssFeed>>(feeds);
        }

        [OperationContract]
        public WebResult UnfollowFeed(string connectionKey, RssFeed feed)
        {
            User user = _sessionWrapper.GetUser(connectionKey);

            if (user == null)
                return new WebResult(WebResult.ErrorCodeList.NOT_LOGUED);
            FeedByUser feedUser = (from item in db.FeedByUsers where item.User == user && item.id_feed == feed.Id select item).SingleOrDefault();

            if (feedUser == null)
                return new WebResult(WebResult.ErrorCodeList.ITEM_NOT_FOUND);
            db.FeedByUsers.DeleteOnSubmit(feedUser);
            db.SubmitChanges();
            return new WebResult();
        }
    }
}
