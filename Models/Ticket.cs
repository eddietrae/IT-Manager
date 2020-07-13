using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace itmanager.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        [Required(ErrorMessage = "Please enter a short description.")]
        [StringLength(50)]
        public string ShortDescription { get; set; }

        [Required(ErrorMessage = "Please enter a detailed description.")]
        public string DetailedDescription { get; set; }

        // Foreign Key to Severity Table
        [Required(ErrorMessage = "Please enter the severity")]
        public string SeverityId { get; set; }
        public string Severity { get; set; }

        // Foreign Key to Status Table
        [Required(ErrorMessage = "Please enter the status")]
        public string StatusId { get; set; }
        public string Status { get; set; }

        // Foreign Key to Store table
        [Required(ErrorMessage = "Please enter the store")]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        // Foreign Key to Employee Table
        [Required(ErrorMessage = "Please enter the employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}