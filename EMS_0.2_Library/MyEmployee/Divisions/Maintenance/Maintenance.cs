﻿namespace EMS_Library.MyEmployee.Divisions
{
    public abstract class Maintenance : Employee
    {
        protected Maintenance(string data) : base(data)
        {
        }

        protected Maintenance(object[] data) : base(data)
        {
        }

        protected Maintenance(Type type, int intId, string stateID, string fName, string lName, string mName, string password, string email, string gender, DateTime birthDate, DateTime created, string employmentStatus, double baseSalary, double salaryModifire, string phoneNumber, string address) : base(type, intId, stateID, fName, lName, mName, password, email, gender, birthDate, created, employmentStatus, baseSalary, salaryModifire, phoneNumber, address)
        {
        }
    }
}
