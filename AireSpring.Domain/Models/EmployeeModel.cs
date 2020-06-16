using System;
using System.ComponentModel.DataAnnotations;

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
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime HireDate { get; set; }


    }
}
