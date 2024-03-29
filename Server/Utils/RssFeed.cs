﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Server.EntityFramwork;
using System.Xml;
using System.ServiceModel.Syndication;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                link = feed.Links[0].Uri.AbsoluteUri,
                description = feed.Description.Text,
                url = uri.AbsoluteUri,

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
            using (var db = new ServerDataContext())
            {
                Item dbItem = null;
                string guid = item.Id;
                bool isNewItem = false;

                if (guid != null)
                {

                    var dbitems = from obj in db.Items where obj.Channel.id == chan.id && obj.guid == guid select obj;
                    if (dbitems.Count() >= 1)
                    {
                        Assert.AreEqual(dbitems.Count(), 1);
                        dbItem = dbitems.Single();
                    }

                }

                if (dbItem == null)
                {
                    isNewItem = true;
                    dbItem = new Item();
                }

                dbItem.id_channel = chan.id;
                dbItem.title = item.Title != null ? item.Title.Text : "";
                dbItem.link = item.Links.Count > 0 ? item.Links[0].Uri.AbsoluteUri : "";
                var date = item.LastUpdatedTime > item.PublishDate ? item.LastUpdatedTime : item.PublishDate;
                dbItem.pubDate = date;
                dbItem.description = item.Summary != null ? item.Summary.Text : "";
                dbItem.guid = item.Id != null ? item.Id : "";
                dbItem.author = item.Authors.Count > 0 ? item.Authors[0].Email : "";
                dbItem.category = item.Categories.Count > 0 ? item.Categories[0].Name : "";
                dbItem.comments = "";

                if (isNewItem)
                    db.Items.InsertOnSubmit(dbItem);
                db.SubmitChanges();
            }
        }

        public static Channel UriToDbChannel(Uri uri)
        {
            if (uri == null)
                return null;

            Channel chan = null;
            using (var db = new ServerDataContext())
            {
                chan = (from channel in db.Channels where channel.url == uri.ToString() select channel).SingleOrDefault();
            }
            if (chan != null)
            {
                UpdateDBChannel(chan);
                return chan;
            }

            return CreateDBChannelWithUri(uri);
        }

        public static void UpdateDBChannel(Channel parChan)
        {
            XmlReader reader = XmlReader.Create(parChan.url);
            SyndicationFeed feed = SyndicationFeed.Load(reader);

            
            using (var db = new ServerDataContext())
            {
                var chanToUpdate = (from chan in db.Channels where chan.id == parChan.id select chan).SingleOrDefault();
                // Mandatory section
                chanToUpdate.title = feed.Title.Text;
                chanToUpdate.link = feed.Links[0].Uri.AbsoluteUri;
                chanToUpdate.description = feed.Description.Text;

                // Not mandatory
                chanToUpdate.lastBuildDate = feed.LastUpdatedTime.DateTime;
                chanToUpdate.image = feed.ImageUrl != null ? feed.ImageUrl.ToString() : null;
            
                db.SubmitChanges();
            }
            if (feed == null)
                return;
            foreach (SyndicationItem item in feed.Items)
            {
                CreateDbItem(item, parChan);
            }
        }
    }
}