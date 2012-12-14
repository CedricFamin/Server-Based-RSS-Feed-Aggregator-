using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Server.Utils;
using Server.Services;

namespace Server.Interface
{
    interface IFeeds
    {
        WebResult<RssFeed> AddNewFeed(string connectionKey, Uri uri);
        WebResult<List<RssFeed>> GetFeeds(string connectionKey);
    }
}
