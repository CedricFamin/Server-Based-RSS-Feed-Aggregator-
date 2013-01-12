using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common.Utils;

namespace Common.DataModel
{
    /// <summary>
    /// Petite class outils permettant de partager des champs de recherche entre divers view model
    /// .... je suis pas super fier de moi mais j'ai pas envie de mettre trop de fromage dans les raviolis
    /// Au moins c'est "Standard"
    /// </summary>
    public class SearchDataModel : BindableObject
    {
        #region CTor/DTor/Singleton
        private static SearchDataModel _instance = new SearchDataModel();
        public static SearchDataModel Instance { get { return _instance; } }
        
        private SearchDataModel()
        {
            SearchBase = "Search Feeds ...";
            Search = SearchBase;
        }

        ~SearchDataModel()
        {
        }
        #endregion

        #region PPTies
        public string SearchBase { get; set; }

        private string _search;

        public string Search
        {
            get { return _search; }
            set { _search = value; RaisePropertyChange("Search"); }
        }
        #endregion

        public bool IsSearchValue(string searchValue)
        {
            return searchValue != SearchBase && searchValue != "";
        }
    }
}
