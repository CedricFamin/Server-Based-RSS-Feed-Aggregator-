namespace Client_Outlook
{
    partial class MainContainerRibbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public MainContainerRibbon()
            : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
            button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(button1_Click);
        }

        void button1_Click(object sender, Microsoft.Office.Tools.Ribbon.RibbonControlEventArgs e)
        {
           
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
            this.RssFeed = this.Factory.CreateRibbonTab();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.button1 = this.Factory.CreateRibbonButton();
            this.RssFeed.SuspendLayout();
            this.group1.SuspendLayout();
            // 
            // RssFeed
            // 
            this.RssFeed.Groups.Add(this.group1);
            this.RssFeed.Label = "Rss Feed";
            this.RssFeed.Name = "RssFeed";
            // 
            // group1
            // 
            this.group1.Items.Add(this.button1);
            this.group1.Label = "group1";
            this.group1.Name = "group1";
            // 
            // button1
            // 
            this.button1.Label = "button1";
            this.button1.Name = "button1";
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

        }

        #endregion

        internal Microsoft.Office.Tools.Ribbon.RibbonTab RssFeed;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
    }

    partial class ThisRibbonCollection
    {
        internal MainContainerRibbon MainContainerRibbon
        {
            get { return this.GetRibbon<MainContainerRibbon>(); }
        }
    }
}
