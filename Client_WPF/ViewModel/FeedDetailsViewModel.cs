using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client_WPF.FeedService;
using Client_WPF.DataModel;

namespace Client_WPF.ViewModel
{
    class FeedDetailsViewModel : BindableObject
    {
        #region Fields
        private List<Item> _items;
        private FeedDetailsDataModel feedDetailsDataModel;
        #endregion

        #region Properties
        public Channel RootChannel { get; private set; }
        public List<Item> Items
        {
            get  { return _items; }
            private set { _items = value; RaisePropertyChange("Items"); }
        }

        public string WindowTitle
        {
            get { return RootChannel.Title + " (" + RootChannel.Link + ")"; }
        }
        #endregion

        public FeedDetailsViewModel()
        {
            RootChannel = new Channel();
            RootChannel.Title = "Titre du channel";
            RootChannel.Description =   "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sit amet eleifend ante." +
                                        "In vel mauris metus, ac viverra lectus. Aenean dui sapien, pretium eu gravida ut, sollicitudin" + 
                                        "et nisi. In hac habitasse platea dictumst. Quisque egestas ligula in lorem sodales sed" + 
                                        " congue turpis varius. Maecenas vel quam at tortor viverra tristique vitae at lorem. Maecenas" + 
                                        " augue augue, convallis tristique congue ut, porta sed felis. Nam nisi libero, vehicula" + 
                                        " ac ultricies quis, imperdiet lobortis tellus.";
            RootChannel.Link = "http://www.google.com";

            Items = new List<Item>();
            for (int i = 0; i < 20; ++i)
            {
                Item item = new Item();
                item.Title = "Titre de l'item " + i.ToString();
                item.Description =  "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nam sit amet eleifend ante." +
                                    "In vel mauris metus, ac viverra lectus. Aenean dui sapien, pretium eu gravida ut, sollicitudin" +
                                    "et nisi. In hac habitasse platea dictumst. Quisque egestas ligula in lorem sodales sed" +
                                    " congue turpis varius. Maecenas vel quam at tortor viverra tristique vitae at lorem. Maecenas" +
                                    " augue augue, convallis tristique congue ut, porta sed felis. Nam nisi libero, vehicula" +
                                    " ac ultricies quis, imperdiet lobortis tellus.";
                Items.Add(item);
            }
        }

        public FeedDetailsViewModel(Channel channel)
        {
            RootChannel = channel;
            feedDetailsDataModel = new FeedDetailsDataModel(channel);
            feedDetailsDataModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(feedDetailsDataModel_PropertyChanged);
            Items = feedDetailsDataModel.Items;
        }

        void feedDetailsDataModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Items")
            {
                Items = (sender as FeedDetailsDataModel).Items;
            }
        }
    }
}
