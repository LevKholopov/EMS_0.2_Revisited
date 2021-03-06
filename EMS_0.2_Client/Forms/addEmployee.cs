using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using EMS_Library.MyEmployee;
using EMS_Library;

namespace EMS_Client.Forms
{
    public partial class addEmployee : Form
    {
        #region Variables
        Bitmap employeeImage;
        Control[] activeControls;
        #endregion
        public addEmployee()
        {
            InitializeComponent();
            activeControls = new Control[] {
                txtID, txtFirstName , txtLastName, txtMiddleName,
                txtGender,txtDateOfBirth,txtAddres,txtPhone,
                txtBaseSalary,txtSalaryModifire,txtEmail,positionBox,txtFile,pictureBox1
            };
        }

        #region Buttons
        /// <summary>
        /// Saving data
        /// </summary>
        private void btnSave_Click(object sender, EventArgs e)
        {
            //Format validation
            if (CheckingDataFields())
            {
                //Addition of a new employee to DB.
                List<string> buffer = new List<string>();
                Action getFreeIdAction = Requests.BuildAction(this, new EMS_Library.Network.DataPacket("", 252), buffer);
                getFreeIdAction.Invoke();

                object[] empParts = new object[] {
                    positionBox.Text,
                    buffer[0],
                    txtID.Text,
                    txtFirstName.Text,
                    txtLastName.Text,
                    txtMiddleName.Text,
                    "PasswordPlaceholder",
                    txtEmail.Text,
                    txtGender.Text,
                    txtDateOfBirth.Text,
                    DateTime.Now,        //created at
                    "1",                 //status
                    txtBaseSalary.Text,
                    txtSalaryModifire.Text,
                    txtPhone.Text,
                    txtAddres.Text
                };

                buffer.Clear();
                Employee emp = Employee.ActivateEmployee(empParts);
                if (emp == null) { MessageBox.Show("Failed to create employee!"); return; }
                string querry = Requests.AddEmployee(emp);

                Action AddEmpAction = Requests.BuildAction(this, new EMS_Library.Network.DataPacket(emp.ToString(), 2), buffer);
                AddEmpAction.Invoke();

                //Rescaling image
                Utility.RescaleImage(employeeImage).Save(Config.FR_Images + $"\\{emp.IntId}{Config.ImageFormat}");
                if(buffer[0]=="1") MessageBox.Show($"{emp.FName} {emp.LName} saved");
            }
            else MessageBox.Show("Incorrect format!");
        }

        /// <summary>
        /// Close button
        /// </summary>
        private void btnX_Click(object sender, EventArgs e) => Close();

        /// <summary>
        /// Clear all fields
        /// </summary>
        private void btnClear_Click(object sender, EventArgs e)
        {
            // החזרת הפאנלים לצבע כחול
            Panel[] panelArr = new Panel[] { panelID, panelFname, panelLname, panelDate, panelAddres, panelPhone, panelEmail, panelBaseSalary
                ,panelSalaryModifire,panelPosition,panelUpload };
            foreach (Panel panel in panelArr)
                panel.BackColor = Color.FromArgb(0, 126, 249);

            foreach (Control control in activeControls)
            {
                if (control is TextBox) ((TextBox)control).Text = "";
                else if (control is ComboBox) ((ComboBox)control).Text = "";
                else if (control is PictureBox) ((PictureBox)control).Image = null;
            }
        }
        /// <summary>
        /// Import picture
        /// </summary>
        private void btnUpload_Click(object sender, EventArgs e)
        {
            OpenFileDialog a = new OpenFileDialog();
            a.ShowDialog();
            string file = a.FileName;
            try
            {
                employeeImage = new Bitmap(file);
                pictureBox1.Image = employeeImage;
                txtFile.Text = file;
            }
            catch { MessageBox.Show("Failed"); }
        }
        #endregion

        private bool CheckingDataFields()
        {
            Dictionary<Panel, bool> test = new Dictionary<Panel, bool>() {
                { panelID, txtID.Text.Parsable(typeof(int)) },
                { panelFname, txtFirstName.Text.Length > 1 },
                { panelLname, txtLastName.Text.Length > 1 },
                { panelDate, txtDateOfBirth.Text.Parsable(typeof(DateTime)) },
                { panelAddres, txtAddres.Text.Length > 1 },
                { panelPhone, txtPhone.Text.Parsable(typeof(int)) },
                { panelEmail, txtEmail.Text.Parsable(typeof(System.Net.Mail.MailAddress)) },
                { panelBaseSalary, txtBaseSalary.Text.Parsable(typeof(int)) },
                { panelSalaryModifire, txtSalaryModifire.Text.Parsable(typeof(double)) },
                { panelPosition, positionBox.Text != "" },
                { panelUpload, pictureBox1 != null }
            };
            foreach (KeyValuePair<Panel, bool> item in test)
            {
                if (!item.Value) item.Key.BackColor = Color.FromArgb(255, 102, 102);
                else { item.Key.BackColor = Color.FromArgb(0, 126, 249); }
            }
            return !test.Values.Contains(false);
        }

        #region Drag Window
        /// <summary>
        /// Controlls form's movement during drag.
        /// </summary>
        void Drag(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panelAddEmployee_MouseDown(object sender, MouseEventArgs e) => Drag(e);
        private void lblAddEmployee_MouseDown(object sender, MouseEventArgs e) => Drag(e);
        #endregion
    }
}
