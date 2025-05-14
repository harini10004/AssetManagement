using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssestManagement.Entity
{
    public class Employee
    {
        private string name;
        private string email;
        private string password;

        public int EmployeeId { get; set; }

        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Name cannot be empty or null.");
                name = value;
            }
        }

        public string Department { get; set; }

        public string Email
        {
            get => email;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || !value.Contains("@"))
                    throw new InvalidDataException("Invalid email format.");
                email = value;
            }
        }

        public string Password
        {
            get => password;
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 6)
                    throw new InvalidDataException("Password must be at least 6 characters long.");
                password = value;
            }
        }

        public Employee() { }

        public Employee(int employeeId, string name, string department, string email, string password)
        {
            EmployeeId = employeeId;
            Name = name;
            Department = department;
            Email = email;
            Password = password;
        }
    }
}
