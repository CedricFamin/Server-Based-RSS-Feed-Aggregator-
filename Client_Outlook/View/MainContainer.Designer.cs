namespace Client_Outlook.View
{
    partial class MainContainer
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.mainContainerUserControl2 = new Client_Outlook.View.MainContainerUserControl();
            this.SuspendLayout();
            // 
            // elementHost1
            // 
            this.elementHost1.AutoSize = true;
            this.elementHost1.Location = new System.Drawing.Point(0, 0);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(1, 1);
            this.elementHost1.TabIndex = 0;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = null;
            // 
            // elementHost2
            // 
            this.elementHost2.AutoSize = true;
            this.elementHost2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.elementHost2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.elementHost2.Location = new System.Drawing.Point(0, 0);
            this.elementHost2.Margin = new System.Windows.Forms.Padding(0);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.elementHost2.Size = new System.Drawing.Size(150, 150);
            this.elementHost2.TabIndex = 1;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.mainContainerUserControl2;
            // 
            // MainContainer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSize = true;
            this.Controls.Add(this.elementHost2);
            this.Controls.Add(this.elementHost1);
            this.Name = "MainContainer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private MainContainerUserControl mainContainerUserControl2;
    }
}
