using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class Employee : User
    {
        public double Salary { get; set; }
        public double HourlyRate { get; set; }

        public string EmployeeRole { get; set; }

        public List<int> UserPermissions { get; set; }


        public void AddUserPermissions(int value)
        {
            UserPermissions.Add(value);

        }

        public void RemoveUserPermissions(int value)
        {
            UserPermissions.Remove(value);
        }

    }
}