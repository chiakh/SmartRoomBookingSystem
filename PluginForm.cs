using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Outlook = Microsoft.Office.Interop.Outlook;
using SmartRoomBookingOutlookAddIn.SmartRoomBookingService;
using SmartRoomBookingOutlookAddIn;
using System.Web.UI.WebControls;

namespace SmartRoomBookingOutlookAddIn
{
    public partial class PluginForm : Form
    {
        DateTime StartTime;
        public PluginForm()
        {
            InitializeComponent();
            BindTime();
           
        }

        private void button_search_Click(object sender, EventArgs e)
        {
            DateTime startDateTime = startDate.Value;
            DateTime endDateTime = endDate.Value;

            DateTime endTime= Convert.ToDateTime(endTimeComboBox.SelectedItem.ToString());
            DateTime startTime = Convert.ToDateTime(startTimeComboBox.SelectedItem.ToString());
            DateTime dateTimeStart;

            dateTimeStart = DateTime.Parse(startDateTime.Month.ToString() + "/" + startDateTime.Day.ToString() + "/" + startDateTime.Year.ToString() + " " + startTime.Hour + ":" + startTime.Minute + ":" + startTime.Second + "." + startTime.Millisecond);

            DateTime dateTimeEnd;
            dateTimeEnd = DateTime.Parse(endDateTime.Month.ToString() + "/" + endDateTime.Day.ToString() + "/" + endDateTime.Year.ToString() + " " + endTime.Hour + ":" + endTime.Minute + ":" + endTime.Second + "." + endTime.Millisecond);


            if (dateTimeStart > dateTimeEnd || dateTimeStart == dateTimeEnd)
            {
               MessageBox.Show("Please enter a valid date time.");
            }
            else
            {
                SmartRoomBookingService.SmartRoomBookingServiceClient smartRoomBookingServiceClient = new SmartRoomBookingService.SmartRoomBookingServiceClient();
                //SmartRoomBookingService2.SmartRoomBookingServiceClient smartRoomBookingServiceClient2 = new SmartRoomBookingService2.SmartRoomBookingServiceClient();

                //List<Room> list = new List<Room>();
                List<String> recipientEmailAddressList = new List<String>();

                string organiserEmail = SmartRoomBookingOutlookAddIn.Globals.ThisAddIn.getOrganiserEmail();

                // This is to retrieve the list of recipients 
                recipientEmailAddressList = SmartRoomBookingOutlookAddIn.Globals.ThisAddIn.GetRecipientEmailAddress();

                //try
                //{
                    // This is to retrieve available roomm
                    IEnumerable<Room> exchangeServerRooms = smartRoomBookingServiceClient.FindAvailableRooms(/*organiserEmail, recipientEmailAddressList.ToArray(),*/startDateTime, endDateTime);

                    foreach (var room in exchangeServerRooms)
                    {
                        roomList.Items.Add(room.Name);
                    }
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show("Something went wrong. Please try again or contact the administrator." + ex);
                //}
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //int time;
            //time = int.Parse(startTimeComboBox.SelectedItem.ToString());
            //time = time + 200;
            //label7.Text = time.ToString();
        }

        private void BindTime()
        {
            // Set the start time (00:00 means 12:00 AM)
            StartTime = DateTime.ParseExact("00:00", "HH:mm", null);
            // Set the end time (23:55 means 11:55 PM)
            DateTime EndTime = DateTime.ParseExact("23:55", "HH:mm", null);
            //Set 30 minutes interval
            TimeSpan Interval = new TimeSpan(0, 30, 0);
            //To set 1 hour interval
            //TimeSpan Interval = new TimeSpan(1, 0, 0);           
            startTimeComboBox.Items.Clear();
            endTimeComboBox.Items.Clear();
            while (StartTime <= EndTime)
            {
                startTimeComboBox.Items.Add(StartTime.ToShortTimeString());
                endTimeComboBox.Items.Add(StartTime.ToShortTimeString());
                StartTime = StartTime.Add(Interval);
            }
            //startTimeComboBox.Items.Insert(0, new ListItem(StartTime.ToShortTimeString(), "0"));
            //endTimeComboBox.Items.Insert(0, new ListItem(StartTime.ToShortTimeString(), "0"));
            startTimeComboBox.SelectedIndex = 0;
            endTimeComboBox.SelectedIndex = 0;
        }

        private void button_confirm_Click(object sender, EventArgs e)
        {

            string room = roomList.GetItemText(roomList.SelectedItem);
            try
            {
                SmartRoomBookingOutlookAddIn.Globals.ThisAddIn.setOutlookFields(startDate.Value, endDate.Value, room, Convert.ToDateTime(startTimeComboBox.SelectedItem.ToString()), Convert.ToDateTime(endTimeComboBox.SelectedItem.ToString()));
            }
            catch(Exception ex)
            {
            }
            this.Close();
        }
    }
}
