﻿namespace EMS_Library
{
    public static class Config
    {
        #region Server_Config
        public const string ServerIP = "127.0.0.1";
        public const int ServerPort = 13000;
        public static string RootDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\EMS_Root";
        public static string FR_Location = RootDirectory + "\\FR_Boot.cmd";
        #endregion

        #region SQL_server_config
        public static string SQLServerName => SQLServerNames[ServerNamesIterator++ % SQLServerNames.Length];
        public static string[] SQLServerNames =
        {
            "DESKTOP-BVFPCJ9\\SQLEXPRESS",
            "LEV-STATPC\\LEVPCMSSQLSERVER",
            "DESKTOP-A6E5597\\SQLEXPRESS",
            "DESKTOP-LL8D68S",
        };
        
        public const string DefaultId = "111111111";
        public const string DefaultPassword = "111111111";

        //Variables
        public static byte ServerNamesIterator = 0;
        public static string SQLConnectionString = default;
        #endregion

        #region SQL_DB_config
        public const string SQLDatabaseName = "EmployeeManagmentDataBase";
        public const string EmployeeDataTable = "Employees";
        public const string EmployeeHourLogsTable = "HourLogs";

        public const string PythonDBConnection = "Driver={SQL Server Native Client 11.0};|Server=DESKTOP-BVFPCJ9\\SQLEXPRESS;|Database=EmployeeManagmentDataBase;|Trusted_Connection=yes;";
        #endregion

        #region Legal
        public static TimeSpan NormalShiftLength = new TimeSpan(0, 8, 0, 0);
        public static TimeSpan MaxShiftLength = new TimeSpan(0, 12, 0, 0);
        #endregion

        public const bool flag = true;
    }
}