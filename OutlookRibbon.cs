using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace SmartRoomBookingOutlookAddIn
{
    public partial class OutlookRibbon
    {
        private void OutlookRibbon_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            PluginForm form = new PluginForm();
            form.Show();

        }
    }
}
