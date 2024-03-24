using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAL.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name Is Required")] 
        public string Name { get; set; }

        [Required(ErrorMessage = "Code Is Required")]
        public string code { get; set; }

        public DateTime DateOfCreation { get; set; }

        public ICollection<Employee> Employees { get; set; } =new HashSet<Employee>();


    }
}
