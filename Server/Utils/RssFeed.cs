using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.EntityFramwork;
using System.Xml;
using System.ServiceModel.Syndication;
using System.ComponentModel;

namespace Server.Utils
{
    public static class RssFeedConverter
    {
        private static Channel CreateDBChannelWithUri(Uri uri)
        {
            XmlReader reader = XmlReader.Create(uri.ToString());
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            if (feed == null)
                return null;

            Channel chan = new Channel()
            {
                // Mandatory section
                title = feed.Title.Text,
                link = feed.Links[0].ToString(),
                description = feed.Description.Text,
                url = uri.ToString(),

                // Not mandatory
                lastBuildDate = feed.LastUpdatedTime.DateTime,
                image = feed.ImageUrl != null ? feed.ImageUrl.ToString() : null,
            };

            using (var db = new ServerDataContext())
            {
                db.Channels.InsertOnSubmit(chan);
                db.SubmitChanges();
            }

            foreach (SyndicationItem item in feed.Items)
            {
                CreateDbItem(item, chan);
            }

            return chan;
        }

        private static void CreateDbItem(SyndicationItem item, Channel chan)
        {
            Item dbItem = new Item()
            {
                id_channel = chan.id,
                title = item.Title != null ? item.Title.Text : null,
                link = item.Links.Count > 0 ? item.Links[0].ToString() : null,
                pubDate = item.PublishDate.DateTime,
                description = item.Summary != null ? item.Summary.Text : null,
                guid = item.Id != null ? item.Id : null,
                author = item.Authors.Count > 0 ? item.Authors[0].ToString() : null,
                category = item.Categories.Count > 0 ? item.Categories[0].ToString() : null,
                comments = null // DO NOT USE
            };

            using (var db = new ServerDataContext())
            {
                db.Items.InsertOnSubmit(dbItem);
                db.SubmitChanges();
            }
        }

        public static Channel UriToDbChannel(Uri uri)
        {
            if (uri == null)
                return null;

            using (var db = new ServerDataContext())
            {
                Channel chan = (from channel in db.Channels where channel.url == uri.ToString() select channel).SingleOrDefault();
                if (chan != null)
                    return chan;
            }

            return CreateDBChannelWithUri(uri);
        }
    }
}