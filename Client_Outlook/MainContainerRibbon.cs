using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Common.DataModel;
using Client_Outlook.ViewModel;

namespace Client_Outlook
{
    public partial class MainContainerRibbon
    {

        #region ViewModel

        private RibbonViewModel _viewModel = null;

        public RibbonViewModel ViewModel
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = new RibbonViewModel();
                    _viewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_userData_PropertyChanged);
                }
                return _viewModel; 
            }
        }
        #endregion

        #region PPTIES
        public bool IsConnected { get; private set; }
        #endregion

        void _userData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                IsConnected = ViewModel.IsConnected;
            }
        }
        
        private void ToggleVIewButton_Click_1(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TaskPane.Visible = !Globals.ThisAddIn.TaskPane.Visible;
        }

        private void ConnectButton_Click(object sender, RibbonControlEventArgs e)
        {
            string[] infos = {EmailEditBox.Text, PasswordEditBox.Text };
            
            ViewModel.Connect.Execute(infos as Object); 
        }

        private void AddFeedButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (IsConnected)
            {
                ViewModel.AddFeed.Execute(FeedEditBox.Text as Object);
            }
        }

        private void RefreshButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (IsConnected)
            {
                // HOW TO DO THIS ?
            }
        }
    }
}
