﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;

namespace EMS_Client.Forms
{
    public partial class newEmail : Form
    {
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
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();
        private void panelNewMail_MouseDown(object sender, MouseEventArgs e) => Drag(e);
        private void lblNewMail_MouseDown(object sender, MouseEventArgs e) => Drag(e);
        #endregion
        // בנאי להודעה חדשה
        public newEmail()
        {
            InitializeComponent();
        }
        string to;
        string subject;

        // בנאי להשיב למייל
        public newEmail(string to, string subject)
        {
            InitializeComponent();
            this.to = to;
            this.subject = subject;
        }
        private void btnX_Click(object sender, EventArgs e) => Close();
        private void btnSend_Click(object sender, EventArgs e)
        {
            //user - "employee.management.system010@gmail.com", pass - "employee.management.system!!@"
            SmtpClient Smtp = new SmtpClient("smtp.gmail.com", 587);
            MailMessage mail = new MailMessage("employee.management.system010@gmail.com", txtTo.Text, txtSubject.Text, richTextBody.Text);
            if (lblFile.Text != "")
            {
                Attachment file = new Attachment(lblFile.Text);
                mail.Attachments.Add(file);
            }
            Smtp.EnableSsl = true;
            //Smtp.UseDefaultCredentials = true;
            Smtp.Credentials = new System.Net.NetworkCredential("employee.management.system010@gmail.com", "employee.management.system!!@");
            Smtp.Send(mail);
            txtTo.Clear();
            txtSubject.Clear();
            richTextBody.Clear();
            lblFile.Text = "";
        }
        private void btnAddFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog opdFile = new OpenFileDialog();
            opdFile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            if (opdFile.ShowDialog() == DialogResult.OK)
            {
                lblFile.Text = opdFile.FileName;
            }
        }
        private void newEmail_Load(object sender, EventArgs e)
        {
            if (to != "" || subject != "")
            {
                txtTo.Text = to;
                txtSubject.Text = subject;
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            selectEmployee se = new selectEmployee(this);
            se.Show();
        }
    }
}
