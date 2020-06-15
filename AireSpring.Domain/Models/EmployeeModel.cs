using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace AireSpring.Domain.Models
{
   public class EmployeeModel
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression("^\\(\\d{3}\\)\\s\\d{3}-\\d{4}", ErrorMessage = "Please enter valid phone no. (999) 999-9999")]
        public string Phone { get; set; }
        
        [DataType(DataType.PostalCode)]
        public string ZipCode { get; set; }

        [DataType(DataType.Date)]
        public string HireDate { get; set; }


    }
}
