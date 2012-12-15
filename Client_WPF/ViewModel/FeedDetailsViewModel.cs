using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Client_WPF.FeedService;

namespace Client_WPF.ViewModel
{
    class FeedDetailsViewModel : BindableObject
    {
        private Channel _rootChannel;

        private List<Item> _items;
        public List<Item> Items
        {
            get  { return _items; }
            private set { _items = value; RaisePropertyChange("Items"); }
        }

        public FeedDetailsViewModel()
        {
        }

        public FeedDetailsViewModel(Channel channel)
        {
            _rootChannel = channel;
        }
    }
}
