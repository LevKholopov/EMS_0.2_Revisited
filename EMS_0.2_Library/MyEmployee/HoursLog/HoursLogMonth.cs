﻿using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace EMS_Library.MyEmployee.HoursLog
{
    /// <summary>
    /// Monthly work-hours log handling.
    /// </summary>
    public class HoursLogMonth
    {
        Employee _employee;
        int _month;
        int _year;
        HoursLogDay[] _days;

        public HoursLogDay[] Days { get => _days; set => _days = value; }
        public int Month => _month;
        public int Year => _year;

        /// <summary>
        /// Provides total amount of hours worked.
        /// </summary>
        public TimeSpan Total
        {
            get
            {
                TimeSpan sum = TimeSpan.Zero;
                if (Days == null) return TimeSpan.Zero;
                foreach (HoursLogDay day in Days)
                {
                    if (day != null)
                        sum += day.Total;
                }

                return sum;
            }
        }

        /// <summary>
        /// Provides total amount of overtime.
        /// </summary>
        public TimeSpan TotalOvertime
        {
            get
            {
                TimeSpan sum = TimeSpan.Zero;
                foreach (HoursLogDay day in Days)
                    if (day != null)
                        sum += day.TotalOvertime;
                return sum;
            }
        }

        /// <summary>
        /// Provides average amount of daily hours worked
        /// </summary>
        public TimeSpan Average
        {
            get
            {
                int count = 0;
                foreach (HoursLogDay day in Days)
                    if (day != null && day.Total > TimeSpan.Zero)
                        count++;
                return Total / count;
            }
        }

        public HoursLogMonth(Employee employee) => _employee = employee;
        public HoursLogMonth(string[] data, Employee employee)
        {
            _employee = employee;
            HoursLogEntry[] entries = Array.ConvertAll(data, x => new HoursLogEntry(x));
            DateTime tempDate = entries[0].Start;
            _month = tempDate.Month;
            _year = tempDate.Year;
            int daysInMonth = DateTime.DaysInMonth(_year, _month);
            _days = new HoursLogDay[daysInMonth];
            for (int i = 0; i < daysInMonth; i++)
                _days[i] = new HoursLogDay(Array.FindAll(entries, x => x.Start.Day == i + 1), new DateTime(_year, _month, i + 1));
        }

        /// <summary>
        /// Adds an entry to monthly log.
        /// </summary>
        /// <param name="entry"></param>
        public void Add(HoursLogEntry entry)
        {
            if (_days == null) 
            { 
                _days = new HoursLogDay[DateTime.DaysInMonth(entry.Start.Year, entry.Start.Month)];
                _days[entry.Start.Day] = new HoursLogDay(new HoursLogEntry[] { entry }, entry.Start.Date); 
                _month = entry.Start.Month;
                _year = entry.Start.Year;
            }
            else if(_days[entry.Start.Day]==null)
                _days[entry.Start.Day] = new HoursLogDay(new HoursLogEntry[] { entry }, entry.Start.Date);
            else 
                _days[entry.Start.Day].Add(entry);
        }

        /// <summary>
        /// Provides string representing the log in JSON format.
        /// </summary>
        public string JSON()
        {
            string hold = $"{{" +
                $"\"InternalID\": \"{_employee.IntId}\"," +
                $"\"StateID\": \"{_employee.StateId}\"," +
                $"\"Full Name\":\"{_employee.FName} {_employee.LName}\"," +
                $"\"Year\":\"{_year}\"," +
                $"\"Month\":\"{_month}\"," +
                $"\"MonthlyHours\":\"{Total}\"," +
                $"\"TotalOvertime\":\"{Config.NormalShiftLength}\"," +
                $"\"Days\":[";
            foreach (HoursLogDay day in _days)
                hold += (day != null ? day.JSON() : "{}") + ',';
            hold = hold.Remove(hold.Length - 1);
            hold += "]}";

            return hold;
        }

        /// <summary>
        /// Provides the log in format that is suitable for the WinForms Table component.
        /// </summary>
        /// <returns></returns>
        public string[][] GetHoursLogTableStructure()
        {
            string[][] Data = new string[DateTime.DaysInMonth(Year, Month)][];
            
            foreach (var day in _days)
                if (day.Entries.IsEmpty())
                    Data[day.Date.Day - 1] = new string[] { $"{day.Date.Date.ToString().Substring(0, 10)}", $"{day.Date.DayOfWeek}", "", "", "" };
                else
                    Data[day.Date.Day - 1] = new string[]
                    {
                            day.Date.ToString().Substring(0,10),
                            day.Date.DayOfWeek.ToString(),

                            day.Entries[0].Start.TimeOfDay.ToString(),
                            day.Entries[0].End.TimeOfDay.ToString(),

                            day.Entries.Length>1?day.Entries[1].Start.TimeOfDay.ToString():"",
                            day.Entries.Length>1?day.Entries[1].End.TimeOfDay.ToString():"",

                            day.Entries.Length>2?day.Entries.Last().Start.TimeOfDay.ToString():"",
                            day.Entries.Length>2?day.Entries.Last().End.TimeOfDay.ToString():"",

                            day.Total.ToString()
                    };
            return Data;
        }
    }
}
