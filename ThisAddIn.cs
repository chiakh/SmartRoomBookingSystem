using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Outlook = Microsoft.Office.Interop.Outlook;
using Office = Microsoft.Office.Core;
using SmartRoomBookingOutlookAddIn;
using Microsoft.Office.Interop.Outlook;

namespace SmartRoomBookingOutlookAddIn
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {

        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
        }

        public string getOrganiserEmail()
        {
            string emailAddress = this.Application.ActiveExplorer().Session.CurrentUser.AddressEntry.GetExchangeUser().PrimarySmtpAddress;
            return emailAddress;
        }

        public List<string> GetRecipientEmailAddress()
        {
            List<string> recipientEmailAddress = new List<string>();
            recipientEmailAddress = getEmailAddress(Application.ActiveInspector().CurrentItem as Outlook.AppointmentItem);
            return recipientEmailAddress;
        }

        public List<string> getEmailAddress(Outlook.AppointmentItem thisMailItem)
        {
            List<string> recipientEmailAddress = new List<string>();

            if (thisMailItem.Recipients.Count > 0)
            {
                foreach (Recipient rec in thisMailItem.Recipients)
                {
                    if ((OlMailRecipientType)rec.Type != OlMailRecipientType.olTo)
                    {
                        recipientEmailAddress.Add(rec.AddressEntry.GetExchangeUser().PrimarySmtpAddress);
                    }
                }
            }
            return recipientEmailAddress;
        }

        public void setOutlookFields(DateTime startDate, DateTime endDate, string room, DateTime startTime, DateTime endTime)
        {
            //Set To: field
            DateTime dateTimeStart;
            //String a = " 04:00 A.M";

            String test = startDate.ToLongDateString();
            String test2 = startDate.ToShortTimeString();
            String test4 = startDate.ToLongTimeString();
            String test3 = startDate.ToShortTimeString();

            dateTimeStart = DateTime.Parse(startDate.Month.ToString() + "/" + startDate.Day.ToString() + "/" + startDate.Year.ToString()  +" "+ startTime.Hour +":" + startTime.Minute +":" + startTime.Second+"." + startTime.Millisecond);

            DateTime dateTimeEnd;
            dateTimeEnd = DateTime.Parse(endDate.Month.ToString() + "/" + endDate.Day.ToString() + "/" + endDate.Year.ToString() + " " + endTime.Hour + ":" + endTime.Minute + ":" + endTime.Second + "." + endTime.Millisecond);

            //if (dateTimeStart > dateTimeEnd)
            //{ 
                
            //}
            //else
            //{
                (Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as Outlook.AppointmentItem).StartInStartTimeZone = dateTimeStart;
                (Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as Outlook.AppointmentItem).EndInEndTimeZone = dateTimeEnd;
                (Globals.ThisAddIn.Application.ActiveInspector().CurrentItem as Outlook.AppointmentItem).Location = room;
            //}
            
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
