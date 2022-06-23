﻿namespace EMS_Client.Forms
{
    partial class AttendanceTable
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panelAttendance = new System.Windows.Forms.Panel();
            this.lblAttendance = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.btnSelect = new System.Windows.Forms.Button();
            this.GridViewAttrndance = new System.Windows.Forms.DataGridView();
            this.date = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Day = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Entry = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Exit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.total_hours = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.getDaysBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.txtName = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblEmployeename = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblMonth = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblID = new System.Windows.Forms.Label();
            this.hoursLogMonthBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dateTime = new System.Windows.Forms.DateTimePicker();
            this.btnShowHours = new System.Windows.Forms.Button();
            this.hoursLogMonthBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.hoursLogMonthBindingSource2 = new System.Windows.Forms.BindingSource(this.components);
            this.hoursLogMonthBindingSource3 = new System.Windows.Forms.BindingSource(this.components);
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelAttendance.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewAttrndance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.getDaysBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursLogMonthBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursLogMonthBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursLogMonthBindingSource2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursLogMonthBindingSource3)).BeginInit();
            this.SuspendLayout();
            // 
            // panelAttendance
            // 
            this.panelAttendance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.panelAttendance.Controls.Add(this.lblAttendance);
            this.panelAttendance.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelAttendance.Location = new System.Drawing.Point(0, 0);
            this.panelAttendance.Name = "panelAttendance";
            this.panelAttendance.Size = new System.Drawing.Size(758, 96);
            this.panelAttendance.TabIndex = 2;
            // 
            // lblAttendance
            // 
            this.lblAttendance.AutoSize = true;
            this.lblAttendance.Font = new System.Drawing.Font("Bahnschrift", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblAttendance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.lblAttendance.Location = new System.Drawing.Point(282, 25);
            this.lblAttendance.Name = "lblAttendance";
            this.lblAttendance.Size = new System.Drawing.Size(161, 35);
            this.lblAttendance.TabIndex = 0;
            this.lblAttendance.Text = "Attendance";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(671, 109);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 1;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnSelect
            // 
            this.btnSelect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelect.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnSelect.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnSelect.Location = new System.Drawing.Point(271, 102);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnSelect.Size = new System.Drawing.Size(172, 30);
            this.btnSelect.TabIndex = 4;
            this.btnSelect.Text = "Select an employee";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // GridViewAttrndance
            // 
            this.GridViewAttrndance.AllowUserToAddRows = false;
            this.GridViewAttrndance.AllowUserToDeleteRows = false;
            this.GridViewAttrndance.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.GridViewAttrndance.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.GridViewAttrndance.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.GridViewAttrndance.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridViewAttrndance.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.date,
            this.Day,
            this.Entry,
            this.Exit,
            this.total_hours,
            this.Column1});
            this.GridViewAttrndance.Location = new System.Drawing.Point(12, 185);
            this.GridViewAttrndance.Name = "GridViewAttrndance";
            this.GridViewAttrndance.ReadOnly = true;
            this.GridViewAttrndance.RowTemplate.Height = 25;
            this.GridViewAttrndance.Size = new System.Drawing.Size(734, 378);
            this.GridViewAttrndance.TabIndex = 5;
            // 
            // date
            // 
            this.date.HeaderText = "Date";
            this.date.Name = "date";
            this.date.ReadOnly = true;
            // 
            // Day
            // 
            this.Day.HeaderText = "Day";
            this.Day.Name = "Day";
            this.Day.ReadOnly = true;
            // 
            // Entry
            // 
            this.Entry.HeaderText = "Entry";
            this.Entry.Name = "Entry";
            this.Entry.ReadOnly = true;
            // 
            // Exit
            // 
            this.Exit.HeaderText = "Exit";
            this.Exit.Name = "Exit";
            this.Exit.ReadOnly = true;
            // 
            // total_hours
            // 
            this.total_hours.HeaderText = "Total hours";
            this.total_hours.Name = "total_hours";
            this.total_hours.ReadOnly = true;
            // 
            // getDaysBindingSource
            // 
            this.getDaysBindingSource.DataMember = "GetDays";
            // 
            // txtName
            // 
            this.txtName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.txtName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtName.ForeColor = System.Drawing.SystemColors.Menu;
            this.txtName.Location = new System.Drawing.Point(126, 153);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(120, 16);
            this.txtName.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.panel1.Location = new System.Drawing.Point(14, 172);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(234, 1);
            this.panel1.TabIndex = 7;
            // 
            // lblEmployeename
            // 
            this.lblEmployeename.AutoSize = true;
            this.lblEmployeename.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.lblEmployeename.Location = new System.Drawing.Point(14, 156);
            this.lblEmployeename.Name = "lblEmployeename";
            this.lblEmployeename.Size = new System.Drawing.Size(106, 15);
            this.lblEmployeename.TabIndex = 6;
            this.lblEmployeename.Text = "Employee\'s name :";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.panel2.Location = new System.Drawing.Point(440, 175);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(174, 1);
            this.panel2.TabIndex = 10;
            // 
            // lblMonth
            // 
            this.lblMonth.AutoSize = true;
            this.lblMonth.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.lblMonth.Location = new System.Drawing.Point(440, 159);
            this.lblMonth.Name = "lblMonth";
            this.lblMonth.Size = new System.Drawing.Size(97, 15);
            this.lblMonth.TabIndex = 9;
            this.lblMonth.Text = "Month and year :";
            // 
            // txtID
            // 
            this.txtID.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.txtID.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtID.ForeColor = System.Drawing.SystemColors.Menu;
            this.txtID.Location = new System.Drawing.Point(305, 157);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(94, 16);
            this.txtID.TabIndex = 17;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.panel4.Location = new System.Drawing.Point(275, 174);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(124, 1);
            this.panel4.TabIndex = 16;
            // 
            // lblID
            // 
            this.lblID.AutoSize = true;
            this.lblID.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.lblID.Location = new System.Drawing.Point(275, 158);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(24, 15);
            this.lblID.TabIndex = 15;
            this.lblID.Text = "ID :";
            // 
            // hoursLogMonthBindingSource
            // 
            this.hoursLogMonthBindingSource.DataSource = typeof(EMS_Library.MyEmployee.HoursLog.HoursLogMonth);
            // 
            // dateTime
            // 
            this.dateTime.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTime.CalendarForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.dateTime.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.dateTime.CalendarTitleBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.dateTime.CalendarTitleForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.dateTime.CalendarTrailingForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.dateTime.CustomFormat = "MM/yyyy";
            this.dateTime.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.dateTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTime.Location = new System.Drawing.Point(536, 148);
            this.dateTime.MaxDate = new System.DateTime(2022, 6, 22, 0, 0, 0, 0);
            this.dateTime.MinDate = new System.DateTime(2022, 1, 1, 0, 0, 0, 0);
            this.dateTime.Name = "dateTime";
            this.dateTime.ShowUpDown = true;
            this.dateTime.Size = new System.Drawing.Size(78, 25);
            this.dateTime.TabIndex = 19;
            this.dateTime.Value = new System.DateTime(2022, 6, 22, 0, 0, 0, 0);
            // 
            // btnShowHours
            // 
            this.btnShowHours.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnShowHours.Font = new System.Drawing.Font("Bahnschrift SemiLight", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btnShowHours.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(126)))), ((int)(((byte)(249)))));
            this.btnShowHours.Location = new System.Drawing.Point(640, 148);
            this.btnShowHours.Name = "btnShowHours";
            this.btnShowHours.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnShowHours.Size = new System.Drawing.Size(106, 30);
            this.btnShowHours.TabIndex = 20;
            this.btnShowHours.Text = "Show hours";
            this.btnShowHours.UseVisualStyleBackColor = true;
            this.btnShowHours.Click += new System.EventHandler(this.btnShowHours_Click);
            // 
            // hoursLogMonthBindingSource1
            // 
            this.hoursLogMonthBindingSource1.DataSource = typeof(EMS_Library.MyEmployee.HoursLog.HoursLogMonth);
            // 
            // hoursLogMonthBindingSource2
            // 
            this.hoursLogMonthBindingSource2.DataSource = typeof(EMS_Library.MyEmployee.HoursLog.HoursLogMonth);
            // 
            // hoursLogMonthBindingSource3
            // 
            this.hoursLogMonthBindingSource3.DataSource = typeof(EMS_Library.MyEmployee.HoursLog.HoursLogMonth);
            // 
            // Column1
            // 
            this.Column1.HeaderText = "Column1";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // AttendanceTable
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(51)))), ((int)(((byte)(73)))));
            this.ClientSize = new System.Drawing.Size(758, 575);
            this.Controls.Add(this.btnShowHours);
            this.Controls.Add(this.dateTime);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblMonth);
            this.Controls.Add(this.txtName);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblEmployeename);
            this.Controls.Add(this.GridViewAttrndance);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.panelAttendance);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AttendanceTable";
            this.Text = "AttendanceTable";
            this.Activated += new System.EventHandler(this.AttendanceTable_Activated);
            this.panelAttendance.ResumeLayout(false);
            this.panelAttendance.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridViewAttrndance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.getDaysBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursLogMonthBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursLogMonthBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursLogMonthBindingSource2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.hoursLogMonthBindingSource3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Panel panelAttendance;
        private Label lblAttendance;
        private Button btnSelect;
        private DataGridView GridViewAttrndance;
        private TextBox txtName;
        private Panel panel1;
        private Label lblEmployeename;
        private Panel panel2;
        private Label lblMonth;
        private TextBox txtID;
        private Panel panel4;
        private Label lblID;
        private Button btnPrint;
        private BindingSource getDaysBindingSource;
        private BindingSource hoursLogMonthBindingSource;
        public DateTimePicker dateTime;
        private Button btnShowHours;
        private DataGridViewTextBoxColumn date;
        private DataGridViewTextBoxColumn Day;
        private DataGridViewTextBoxColumn Entry;
        private DataGridViewTextBoxColumn Exit;
        private DataGridViewTextBoxColumn total_hours;
        private BindingSource hoursLogMonthBindingSource1;
        private BindingSource hoursLogMonthBindingSource2;
        private DataGridViewTextBoxColumn Column1;
        private BindingSource hoursLogMonthBindingSource3;
    }
}