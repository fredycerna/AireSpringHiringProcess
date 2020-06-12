using System;
using System.Collections.Generic;
using System.Text;

namespace AireSpring.Data.Models
{
   public class Employee
    {
        public int EmployeeId { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Phone { get; set; }

        public string Zip { get; set; }

        public DateTime HireDate { get; set; }

    }
}
