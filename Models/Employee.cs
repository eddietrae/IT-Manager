using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations.Schema;

namespace itmanager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Authority { get; set; }

        [Required]
        public string Password { get; set; }

        // Foreign Key to Store Table
        [Required]
        public int StoreId { get; set; }
        public Store Store { get; set; }
    }
}
