using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;

using Microsoft.Office.Tools;
using Microsoft.Office.Interop.Outlook;
using Microsoft.Office.Core;
using Client_Outlook.View;

namespace Client_Outlook
{
   
    public partial class ThisAddIn
    {
        public Microsoft.Office.Tools.CustomTaskPane TaskPane { get; private set; }

        Outlook.Explorer explorer;

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            explorer = this.Application.ActiveExplorer();

            TaskPane = Globals.ThisAddIn.CustomTaskPanes.Add(new MainContainer(), "Demo", explorer);
            TaskPane.Visible = false;
            TaskPane.Width = 245;

            //You can set the docking position of the Panel
            TaskPane.DockPosition = Microsoft.Office.Core.MsoCTPDockPosition.msoCTPDockPositionRight;

            //You can also restrict the docking position change.
            TaskPane.DockPositionRestrict = Microsoft.Office.Core.MsoCTPDockPositionRestrict.msoCTPDockPositionRestrictNone;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            this.CustomTaskPanes.Remove(TaskPane);
            this.Application = null;
        }

        #region VSTO generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }
        
        #endregion
    }
}
