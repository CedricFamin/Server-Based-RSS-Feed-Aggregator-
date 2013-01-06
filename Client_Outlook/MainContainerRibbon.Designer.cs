namespace Client_Outlook
{
    partial class MainContainerRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MainContainerRibbon() : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
        }

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainContainerRibbon));
            this.RssFeed = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.ToggleVIewButton = this.Factory.CreateRibbonButton();
            this.group2 = this.Factory.CreateRibbonGroup();
            this.EmailEditBox = this.Factory.CreateRibbonEditBox();
            this.PasswordEditBox = this.Factory.CreateRibbonEditBox();
            this.ConnectButton = this.Factory.CreateRibbonButton();
            this.Feeds = this.Factory.CreateRibbonGroup();
            this.FeedEditBox = this.Factory.CreateRibbonEditBox();
            this.AddFeedButton = this.Factory.CreateRibbonButton();
            this.RefreshButton = this.Factory.CreateRibbonButton();
            this.RssFeed.SuspendLayout();
            this.group1.SuspendLayout();
            this.group2.SuspendLayout();
            this.Feeds.SuspendLayout();
            // 
            // RssFeed
            // 
            this.RssFeed.Groups.Add(this.group1);
            this.RssFeed.Groups.Add(this.group2);
            this.RssFeed.Groups.Add(this.Feeds);
            this.RssFeed.Label = "Rss Feed";
            this.RssFeed.Name = "RssFeed";
            // 
            // group1
            // 
            this.group1.Items.Add(this.ToggleVIewButton);
            this.group1.Name = "group1";
            // 
            // ToggleVIewButton
            // 
            this.ToggleVIewButton.Image = ((System.Drawing.Image)(resources.GetObject("ToggleVIewButton.Image")));
            this.ToggleVIewButton.Label = "Show/Hide";
            this.ToggleVIewButton.Name = "ToggleVIewButton";
            this.ToggleVIewButton.ShowImage = true;
            this.ToggleVIewButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ToggleVIewButton_Click_1);
            // 
            // group2
            // 
            this.group2.Items.Add(this.EmailEditBox);
            this.group2.Items.Add(this.PasswordEditBox);
            this.group2.Items.Add(this.ConnectButton);
            this.group2.Label = "Account";
            this.group2.Name = "group2";
            // 
            // EmailEditBox
            // 
            this.EmailEditBox.Label = "Email :";
            this.EmailEditBox.Name = "EmailEditBox";
            this.EmailEditBox.Text = null;
            // 
            // PasswordEditBox
            // 
            this.PasswordEditBox.Label = "Password :";
            this.PasswordEditBox.Name = "PasswordEditBox";
            this.PasswordEditBox.Text = null;
            // 
            // ConnectButton
            // 
            this.ConnectButton.Image = ((System.Drawing.Image)(resources.GetObject("ConnectButton.Image")));
            this.ConnectButton.Label = "Connect";
            this.ConnectButton.Name = "ConnectButton";
            this.ConnectButton.ShowImage = true;
            this.ConnectButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.ConnectButton_Click);
            // 
            // Feeds
            // 
            this.Feeds.Items.Add(this.FeedEditBox);
            this.Feeds.Items.Add(this.AddFeedButton);
            this.Feeds.Items.Add(this.RefreshButton);
            this.Feeds.Label = "Feeds";
            this.Feeds.Name = "Feeds";
            // 
            // FeedEditBox
            // 
            this.FeedEditBox.Label = "Feed Url";
            this.FeedEditBox.Name = "FeedEditBox";
            this.FeedEditBox.Text = null;
            // 
            // AddFeedButton
            // 
            this.AddFeedButton.Image = ((System.Drawing.Image)(resources.GetObject("AddFeedButton.Image")));
            this.AddFeedButton.Label = "Add Feed";
            this.AddFeedButton.Name = "AddFeedButton";
            this.AddFeedButton.ShowImage = true;
            this.AddFeedButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.AddFeedButton_Click);
            // 
            // RefreshButton
            // 
            this.RefreshButton.Image = ((System.Drawing.Image)(resources.GetObject("RefreshButton.Image")));
            this.RefreshButton.Label = "Refresh";
            this.RefreshButton.Name = "RefreshButton";
            this.RefreshButton.ShowImage = true;
            this.RefreshButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.RefreshButton_Click);
            // 
            // MainContainerRibbon
            // 
            this.Name = "MainContainerRibbon";
            this.RibbonType = "Microsoft.Outlook.Explorer";
            this.Tabs.Add(this.RssFeed);
            this.RssFeed.ResumeLayout(false);
            this.RssFeed.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.group2.ResumeLayout(false);
            this.group2.PerformLayout();
            this.Feeds.ResumeLayout(false);
            this.Feeds.PerformLayout();

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab RssFeed;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ToggleVIewButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group2;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox EmailEditBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox PasswordEditBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ConnectButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup Feeds;
        internal Microsoft.Office.Tools.Ribbon.RibbonEditBox FeedEditBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton AddFeedButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton RefreshButton;
    }

    partial class ThisRibbonCollection
    {
        internal MainContainerRibbon MainContainerRibbon
        {
            get { return this.GetRibbon<MainContainerRibbon>(); }
        }
    }
}
