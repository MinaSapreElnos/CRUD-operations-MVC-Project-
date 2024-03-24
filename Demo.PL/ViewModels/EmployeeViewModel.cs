using Demo.DAL.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Http;

namespace Demo.PL.ViewModels
{
    public class EmployeeViewModel  
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [MaxLength(50, ErrorMessage = "MaxLength is 50 char")]

        [MinLength(5, ErrorMessage = "MinLength is 5 char")]
        public string Name { get; set; }


        [Range(22, 35, ErrorMessage = "Age is required between 22 to 35")]
        public int Age { get; set; }

        [RegularExpression("^[0-9]{1,3}-[a-zA-Z]{5,10}-[a-zA-Z]{4,10}-[a-zA-Z]{5,10}$",
            ErrorMessage = "Adrees must be like 123-street-city-country")]
        public string Address { get; set; }


        public string ImageName { get; set; } 

        public IFormFile Image { get; set; }


        [DataType(DataType.Currency)]
        public decimal selary { get; set; }


        public bool IsActive { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        

        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }

        public Department Department { get; set; }
    }
}
