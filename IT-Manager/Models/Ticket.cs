using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace itmanager.Models
{
    // setting up ticket class with all fields required
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
        public Severity Severity { get; set; }

        // Foreign Key to Status Table
        [Required(ErrorMessage = "Please enter the status")]
        public string StatusId { get; set; }
        public Status Status { get; set; }

        // Foreign Key to Store table
        [Required(ErrorMessage = "Please enter the store")]
        public int StoreId { get; set; }
        public Store Store { get; set; }

        public string Employee { get; set; }

        // The input for this is the DateTime.Now on the form if applicable
        public DateTime CreationDate { get; set; }

        // Takes a DateTime and returns a double of how many days have passed since the parameter
        public double DaysSinceCreation(DateTime CreationDate)
        {
            var days = (DateTime.Now - CreationDate).TotalDays;
            days = Math.Round(days); // rounding to nearest whole number
            return days;
        }
    }
}