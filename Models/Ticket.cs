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

        // Wondering if this should be another class to restrict the options for severity?
        [Required(ErrorMessage = "Please enter the severity")]
        public string Severity { get; set; }

        // Wondering if this should be another class to restrict the optiosn for status?
        [Required(ErrorMessage = "Please enter the status")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Please enter the store")]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        [Required(ErrorMessage = "Please enter the employee")]
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
    }
}