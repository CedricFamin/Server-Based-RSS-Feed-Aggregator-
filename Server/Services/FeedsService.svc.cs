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
using Server.Data;

namespace Server.Services
{
    [ServiceContract]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class FeedsService : IFeeds
    {
        #region Database
        EntityFramwork.ServerDataContext db = new EntityFramwork.ServerDataContext();
        #endregion

        SessionWrapper _sessionWrapper = new SessionWrapper();

        [OperationContract]
        public WebResult<Channel> AddNewFeed(string connectionKey, Uri uri)
        {
            EntityFramwork.User user = _sessionWrapper.GetUser(connectionKey);
            if (user == null) return new WebResult<Channel>(WebResult.ErrorCodeList.NOT_LOGUED);

            EntityFramwork.Channel dbChan = RssFeedConverter.UriToDbChannel(uri);
            if (dbChan == null)
                return new WebResult<Channel>(WebResult.ErrorCodeList.CANNOT_CREATE_FEED);

            EntityFramwork.ChannelXUser cxu = new EntityFramwork.ChannelXUser()
            {
                id_user = user.id,
                id_channel = dbChan.id
            };

            db.ChannelXUsers.InsertOnSubmit(cxu);
            db.SubmitChanges();

            return new WebResult<Channel>(new Channel(dbChan));
        }

        [OperationContract]
        public WebResult<List<Channel>> GetFeeds(string connectionKey)
        {
            EntityFramwork.User user = _sessionWrapper.GetUser(connectionKey);
            if (user == null) return new WebResult<List<Channel>>(WebResult.ErrorCodeList.NOT_LOGUED);

            List<Channel> chans = new List<Channel>();

            var dbChans = from chan in db.ChannelXUsers where chan.User == user select chan.Channel;
            foreach (var dbChan in dbChans)
            {
                chans.Add(new Channel(dbChan));
            }

            return new WebResult<List<Channel>>(chans);
        }
        
        [OperationContract]
        public WebResult UnfollowFeed(string connectionKey, Channel feed)
        {
            EntityFramwork.User user = _sessionWrapper.GetUser(connectionKey);
            if (user == null) return new WebResult(WebResult.ErrorCodeList.NOT_LOGUED);

            var cxu = from item in db.ChannelXUsers where item.User == user && item.id_channel == feed.Id select item;

            if (cxu != null)
            {
                db.ChannelXUsers.DeleteAllOnSubmit(cxu);
                db.SubmitChanges();
                return new WebResult();
            }

            return new WebResult(WebResult.ErrorCodeList.CANNOT_GET_FEED);
            
        }

        [OperationContract]
        public WebResult<List<Item>> GetFeedItems(string connectionKey, Channel feed)
        {
            if (feed == null)
                return new WebResult<List<Item>>(WebResult.ErrorCodeList.INVALID_PARAMETER);

            EntityFramwork.User user = _sessionWrapper.GetUser(connectionKey);
            if (user == null) return new WebResult<List<Item>>(WebResult.ErrorCodeList.NOT_LOGUED);
            
            List<Item> items = new List<Item>();

            var dbItems = from item in db.Items where item.id_channel == feed.Id select item;
            foreach (var dbItem in dbItems)
            {
                items.Add(new Item(dbItem, user));
            }

            return new WebResult<List<Item>>(items);
        }

        [OperationContract]
        public WebResult ReadItem(string connectionKey, Item item)
        {
            if (item == null)
                return new WebResult<List<Item>>(WebResult.ErrorCodeList.INVALID_PARAMETER);

            EntityFramwork.User user = _sessionWrapper.GetUser(connectionKey);
            if (user == null) return new WebResult<List<Item>>(WebResult.ErrorCodeList.NOT_LOGUED);

            var alreadyRead = (from dbItem in db.ItemReads where dbItem.id_item == item.Id && dbItem.User == user select item).SingleOrDefault() != null;
            if (alreadyRead)
                return new WebResult();

            EntityFramwork.ItemRead ir = new EntityFramwork.ItemRead()
            {
                id_user = user.id,
                id_item = item.Id
            };
            db.ItemReads.InsertOnSubmit(ir);
            db.SubmitChanges();
            return new WebResult();
        }
    }
}
