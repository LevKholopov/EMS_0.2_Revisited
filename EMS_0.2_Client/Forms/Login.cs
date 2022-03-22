﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using EMS_Library;
using EMS_Library.Network;
using EMS_Library.MyEmployee;

namespace EMS_Client.Forms
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            //Debug
            txtIntId.Text = Config.DefaultId;
            txtPassword.Text = Config.DefaultPassword;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            CancellationTokenSource CXL_Src = new CancellationTokenSource();
            CancellationToken CXL = CXL_Src.Token;

            //Debug
            txtIntId.Text = Config.DefaultId;
            txtPassword.Text = Config.DefaultPassword;
            string querry = Requests.SelectEmployee(new Dictionary<string, string> 
            { 
                {"_intId", txtIntId.Text},
                {"_password",txtPassword.Text }
            });
            List<string> buffer = new List<string>();

            Action action = Requests.BuildAction(this, new DataPacket(querry, 1), buffer, false);
            action.Invoke();


            string querry2 = Requests.UpdateEmployee(
                new Dictionary<string, string> { { "_intId", 111111112.ToString() } },
                new Dictionary<string, string> { { "_intId", 111111111.ToString() } }
                );
            List<string> list = new List<string>();
            Action delete = Requests.BuildAction(this, new DataPacket(querry2, 3), list, false);
            delete.Invoke();


            EMS_ClientMainScreen.CurEmployee = Employee.ActivateEmployee(buffer[0].Split(','));
            MessageBox.Show("Hello\n"+EMS_ClientMainScreen.CurEmployee.FName);
            Close();


        }
    }

}
