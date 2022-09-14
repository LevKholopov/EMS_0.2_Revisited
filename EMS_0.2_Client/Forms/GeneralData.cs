﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using EMS_Library.MyEmployee.HoursLog;
using EMS_Library;
using System.Windows.Forms.VisualStyles;

namespace EMS_Client.Forms
{
    public partial class GeneralData : Form
    {
        private ProgressBar[] progressBars;
        private Label[] labels;
        private HoursLogMonth[] data;
        private bool employeeChanged = false; //Avoiding unnesasery requests for pictures.
        private bool firtsOpen = true; //Avoiding condition checks on first load.

        /// <summary>
        /// Constructor for GeneralData screen.
        /// </summary>
        public GeneralData()
        {
            InitializeComponent();

            //Aggregating controlls into collections for later use
            progressBars = new ProgressBar[] { monthBar0, monthBar1, monthBar2, monthBar3, monthBar4, monthBar5, monthBar6, monthBar7, monthBar8, monthBar9, monthBar10, monthBar11 };
            labels = new Label[] { lblMonthData0, lblMonthData1, lblMonthData2, lblMonthData3, lblMonthData4, lblMonthData5, lblMonthData6, lblMonthData7, lblMonthData8, lblMonthData9, lblMonthData10, lblMonthData11 };

            //Priming fields
            foreach (Label label in labels) label.Text = String.Empty;
            List<int> years = new List<int>();
            for (int i = Config.MinDate.Year; i <= DateTime.Now.Year; i++)
                years.Add(i);
            yearPicker.DataSource = years;
            yearPicker.SelectedIndex = years.Count - 1;
            firtsOpen = false;
        }

        /// <summary>
        /// Button for selecting employee.
        /// </summary>
        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectEmployee select_Employee = new selectEmployee(this);
            select_Employee.Show();
            employeeChanged = true;
        }

        /// <summary>
        /// Triggered when selecting a year.
        /// </summary>
        private void yearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!firtsOpen) Fill();
        }

        /// <summary>
        /// Fills the screen with apropriate data.
        /// </summary>
        public void Fill()
        {
            if (employeeChanged) //Only request picture if employee changed to reduce traffic
            {
                pictureBox1.Image = (Bitmap)new ImageConverter().ConvertFrom(Requests.GetImage(new EMS_Library.Network.DataPacket(EMS_ClientMainScreen.employee.IntId.ToString(), 6)));
                employeeChanged = false;
                Array.ConvertAll(progressBars, x => x.Value = 0);
                Array.ConvertAll(labels, x => x.Text = "");
                data = null;
                lblEmpData.Text = "";
            }

            // Retrieves selected employee data from the server
            if (EMS_ClientMainScreen.employee == null) { MessageBox.Show("No employee selected!"); return; }
            data = new HoursLogMonth[12];
            string[][] responce = Array.ConvertAll(Requests.RequestFromServer(Requests.GetYearlyHourLog(EMS_ClientMainScreen.employee.IntId, (int)yearPicker.SelectedValue), 11), x => x.Split(','));

            //If data is available
            if (responce[0][0] != "-1")
            {
                //Constructing 12 mothly logs
                foreach (string[] x in responce)
                {
                    HoursLogEntry entry = new HoursLogEntry(x.Aggregate((aggregate, elem) => { return aggregate + ", " + (elem != null ? elem : "NULL"); }));
                    if (data[entry.Start.Month] == null)
                    {
                        data[entry.Start.Month] = new HoursLogMonth(EMS_ClientMainScreen.employee);
                        data[entry.Start.Month].Add(entry);
                    }
                    else { data[entry.Start.Month].Add(entry); }
                }

                if (data != null)
                {
                    //Fill graph data
                    for (int i = 0; i < progressBars.Length; i++)
                    {
                        if (data[i] != null && data[i].Days.Length != 0)
                        {
                            // = total hours worked / (Max work hours in month / 100%)
                            double value = (data[i].Total.TotalHours / (Config.MaxShiftLength.TotalHours * Config.WorkDaysInWeek * (DateTime.DaysInMonth(data[i].Year, data[i].Month) / 7) / 100));
                            progressBars[i].Value = (int)value;
                            labels[i].Text = $"{value:F2}";
                        }
                        else progressBars[i].Value = 0;
                    }

                }

                string temptot = $"{(float)data.Sum(x => x?.Total.TotalHours):F2}";
                string Total = $"overtime: {(float)data.Sum(x => x?.TotalOvertime.TotalHours):F2}\n";
                string Daily = $"average: {(float)data.Sum(x => x?.Average.TotalHours) / 12:F2} hours";

                string empData = $"{EMS_ClientMainScreen.employee.LName} " +
                    $"{EMS_ClientMainScreen.employee.FName}:\n" +
                    $"Total hours worked: {(float)data.Sum(x => x?.Total.TotalHours):F2}\n" +
                    $"Total overtime: {(float)data.Sum(x => x?.TotalOvertime.TotalHours):F2}\n" +
                    $"Daily average: {(float)data.Sum(x => x?.Average.TotalHours) / data.Sum(x => x == null ? 0 : 1):F2} hours";
                lblEmpData.Text = empData;
            }

            
        }
    }
}
