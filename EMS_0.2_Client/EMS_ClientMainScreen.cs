﻿using EMS_Client.Forms;
using System.Runtime.InteropServices;

namespace EMS_Client
{
    public partial class EMS_ClientMainScreen : Form
    {

        #region Variables
        public static Stack<Form> PrimaryForms = new Stack<Form>();
        public static string CurEmployee;

        #endregion
        #region Drag Window
        /// <summary>
        /// Controlls form movement during drag.
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
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        #endregion

        // משתנה עזר ששומר את החלון הנוכחי
        private Form activeForm;
        // סימון הכפתורים
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);


        public EMS_ClientMainScreen()
        {
            InitializeComponent();
            PrimaryForms.Push(this);
            // סימון הכפתורים
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
        }
        public void openChildForm(Form child, object btnSender)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = child;
            child.TopLevel = false;
            child.FormBorderStyle = FormBorderStyle.None;
            child.Dock = DockStyle.Fill;
            this.panelDesktop.Controls.Add(child);
            this.panelDesktop.Tag = child;
            child.BringToFront();
            child.Show();
        }
        private void EMS_ClientMainScreen_Load(object sender, EventArgs e)
        {

            Login login = new Login();
            login.ShowDialog();
        }

        private void btnExit_Click_1(object sender, EventArgs e) => Close();

        private void changeButton(Button x,Form form, object sender)
        {
            panelStyle.Height = x.Height;
            panelStyle.Top = x.Top;
            x.BackColor = Color.FromArgb(46, 51, 73);
            openChildForm(form , sender);
            x.BackColor = Color.FromArgb(46, 51, 73);
            if (panelDesktop.BackgroundImage != null)
            {
                panelDesktop.BackgroundImage = null;
            }
        }

        private void btnEditingEmployee_Click(object sender, EventArgs e)
        {
            changeButton(btnEditingEmployee, new EditingEmployee(), sender);
        }

        private void btnMail_Click(object sender, EventArgs e)
        {
            changeButton(btnMail, new Mail(), sender);

        }

        private void btnData_Click(object sender, EventArgs e)
        {
            changeButton(btnData, new GeneralData(), sender);

        }

        private void btnAttendence_Click(object sender, EventArgs e)
        {
            changeButton(btnAttendence, new AttendanceTable(), sender);

        }

        private void btnEditingEmployee_Leave(object sender, EventArgs e)
        {
            btnEditingEmployee.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void btnMail_Leave(object sender, EventArgs e)
        {
            btnMail.BackColor = Color.FromArgb(24, 30, 54);

        }

        private void btnData_Leave(object sender, EventArgs e)
        {
            btnData.BackColor = Color.FromArgb(24, 30, 54);

        }

        private void btnAttendence_Leave(object sender, EventArgs e)
        {
            btnAttendence.BackColor = Color.FromArgb(24, 30, 54);

        }

        private void panelForUser_MouseDown(object sender, MouseEventArgs e)
        {
            Drag(e);
        }

        private void panelDesktop_MouseDown(object sender, MouseEventArgs e)
        {
            Drag(e);

        }
    }
}