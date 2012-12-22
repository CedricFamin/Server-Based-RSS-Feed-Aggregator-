using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Utils;
using Server.Services;
using Server.Data;

namespace Server.Interface
{
    interface IFeeds
    {
        WebResult<Channel> AddNewFeed(string connectionKey, Uri uri);
        WebResult<List<Channel>> GetFeeds(string connectionKey);
        WebResult<List<Channel>> GetAllFeeds();
        WebResult UnfollowFeed(string connectionKey, Channel feed);
        WebResult<List<Item>> GetFeedItems(string connectionKey, Channel feed);
        WebResult ReadItem(string connectionKey, Item item);
    }
}
