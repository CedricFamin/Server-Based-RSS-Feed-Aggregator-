using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;

namespace Server.Data
{
    [DataContract]
    public class Channel
    {
        public Channel(EntityFramwork.Channel dbChan)
        {
            Id = dbChan.id;
            Title = dbChan.title;
            Description = dbChan.description;
            Link = dbChan.link;
            Url = dbChan.url;
            PubDate = dbChan.pubDate;
            LastBuildDate = dbChan.lastBuildDate;
            Image = dbChan.image;
            Enclosure = dbChan.enclosure;
        }
#region DataMember
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public DateTime? PubDate { get; set; }
        [DataMember]
        public DateTime? LastBuildDate { get; set; }
        [DataMember]
        public string Image { get; set; }
        [DataMember]
        public string Enclosure { get; set; }
#endregion
    }

    [DataContract]
    public class Item
    {
        public Item(EntityFramwork.Item dbItem, EntityFramwork.User user)
        {
            Id = dbItem.id;
            IdChannel = dbItem.id_channel;
            Title = dbItem.title;
            PubDate = dbItem.pubDate;
            Description = dbItem.description;
            GUID = dbItem.guid;
            Author = dbItem.author;
            Category = dbItem.category;
            Comments = dbItem.comments;

            using (var db = new EntityFramwork.ServerDataContext())
            {
                Read = (from item in db.ItemReads where item.id_item == Id && item.User == user select item).SingleOrDefault() != null;
            }
        }

        #region DataMember
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdChannel { get; set; }
        [DataMember]
        public string Title { get; set; }
        [DataMember]
        public string Link { get; set; }
        [DataMember]
        public DateTimeOffset? PubDate { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public string GUID { get; set; }
        [DataMember]
        public string Author { get; set; }
        [DataMember]
        public string Category { get; set; }
        [DataMember]
        public string Comments { get; set; }
        [DataMember]
        public bool Read { get; set; }
        #endregion
    }

    [DataContract]
    public class ChannelXUser
    {
        #region DataMember
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdChannel { get; set; }
        [DataMember]
        public int IdFeed { get; set; }
        #endregion
    }

    [DataContract]
    public class ReadState
    {
        #region DataMember
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdFeed { get; set; }
        [DataMember]
        public int IdUser { get; set; }
        #endregion
    }
}