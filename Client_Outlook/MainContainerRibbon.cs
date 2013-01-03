using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Common.DataModel;

namespace Client_Outlook
{
    public partial class MainContainerRibbon
    {

        private UserDataModel _userData = null;

        private UserDataModel UserData
        {
            get {
                if (_userData == null)
                {
                    _userData = new UserDataModel();
                    _userData.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(_userData_PropertyChanged);
                }
                return _userData; 
            }
        }

        void _userData_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsConnected")
            {
                bool isConnected = UserData.IsConnected;
                isConnected = isConnected;
            }
        }
        
        private void ToggleVIewButton_Click_1(object sender, RibbonControlEventArgs e)
        {
            Globals.ThisAddIn.TaskPane.Visible = !Globals.ThisAddIn.TaskPane.Visible;
        }

        private void ConnectButton_Click(object sender, RibbonControlEventArgs e)
        {
            UserData.Login(EmailEditBox.Text, PasswordEditBox.Text);
        }
    }
}
