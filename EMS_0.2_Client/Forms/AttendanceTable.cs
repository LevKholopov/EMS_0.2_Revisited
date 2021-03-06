using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EMS_Library.MyEmployee.HoursLog;
using System.Diagnostics;

namespace EMS_Client.Forms
{
    public partial class AttendanceTable : Form
    {
        #region Variables
        public static bool fill = false;
        public HoursLogMonth log;
        #endregion

        public AttendanceTable()
        {
            InitializeComponent();
        }

        // בדאבל קליל - נפתח חלון עריכת שעות עבודה
        public void GridViewAttrndance_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            string idEmployee = EMS_ClientMainScreen.employee.IntId.ToString();
            int y = GridViewAttrndance.CurrentCell.RowIndex;
            string[] hold = new string[] { idEmployee, GridViewAttrndance.SelectedCells[0].Value.ToString(), GridViewAttrndance.SelectedCells[2].Value.ToString(), GridViewAttrndance.SelectedCells[3].Value.ToString() };
            EditHours editHours = new EditHours(hold);
            editHours.Show();
        }

        #region Buttons
        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectEmployee select_Employee = new selectEmployee(this);
            select_Employee.ShowDialog();
            Fill();
        }
        private void btnShowHours_Click(object sender, EventArgs e)
        {
            if (EMS_ClientMainScreen.employee == null)
            { MessageBox.Show("Please select a employee"); return; }

            GridViewAttrndance.Rows.Clear();
            BuildLog();
            if (log != null)
            {
                string[][] tmpGetAttendanceTable = log.GetHoursLogTableStructure();
                foreach (string[] item in tmpGetAttendanceTable)
                    GridViewAttrndance.Rows.Add(item[0], item[1], item[2], item[3], item[4]);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (EMS_ClientMainScreen.employee == null)
            {
                MessageBox.Show("Please select a employee");
                return;
            }
            BuildLog();
            if (log != null)
                File.WriteAllText(EMS_Library.Config.RootDirectory + "\\log.json", log.JSON());
            GenerateXlsxLog();
        }
        #endregion

        #region Supplemental
        public void Fill()
        {
            txtName.Text = EMS_ClientMainScreen.employee.LName.ToString() + " " + EMS_ClientMainScreen.employee.FName.ToString();
            txtID.Text = EMS_ClientMainScreen.employee.IntId.ToString();
        }
        private void BuildLog()
        {
            List<string> buffer = new List<string>();
            DateTime temp = DateTime.Parse("01/" + dateTime.Text);
            string querry = Requests.GetHourLogs(EMS_ClientMainScreen.employee.IntId, temp.Year, temp.Month);
            Action action = Requests.BuildAction(this, new EMS_Library.Network.DataPacket(querry, 5), buffer, false);

            action.Invoke();
            if (buffer[0] != "-1")
                log = new HoursLogMonth(buffer.ToArray(), EMS_ClientMainScreen.employee);
            else log = null;
        }

        /// <summary>
        /// Generates and opens hours log in Excel format.
        /// </summary>
        public void GenerateXlsxLog()
        {
            Thread xlsxBuilder = new Thread(() =>
            {
                File.WriteAllText(Directory.GetCurrentDirectory()+"\\TempClientConfig.txt",$"HoursLog.xlsx_output_directory={EMS_Library.Config.RootDirectory}");

                Process writingXlsx = new Process();
                writingXlsx.StartInfo.FileName = "main.py";
                writingXlsx.StartInfo.UseShellExecute = true;
                writingXlsx.Start();
                while (!writingXlsx.HasExited) { }

                Process PDFProcess = new Process();
                PDFProcess.StartInfo.FileName = $"{EMS_Library.Config.RootDirectory}\\{ EMS_ClientMainScreen.employee.IntId}.pdf";
                PDFProcess.StartInfo.UseShellExecute = true;
                PDFProcess.Start();

                while (!writingXlsx.HasExited) { }

                //Release of resouces
                writingXlsx.Dispose();
                PDFProcess.Dispose();
            });
            xlsxBuilder.Start();
        }
        #endregion
    }
}
