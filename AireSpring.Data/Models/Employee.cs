using AireSpring.Data.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace AireSpring.Data.Models
{
   public class Employee : DbEntity
    {       
        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string Phone { get; set; }

        public string ZipCode { get; set; }

        public DateTime HireDate { get; set; }

    }
}
