using System;
using Xunit;
using itmanager;
using itmanager.Models;
using itmanager.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace XUnitTestProject
{
    public class ItUnitTest
    {
        [Fact] // Checks if valid input passes for DaysSinceCreation()
        public void DaysSinceCreation_Valid()
        {
            // Arrange
            Ticket ticket = new Ticket();
            ticket.CreationDate = DateTime.Now.AddDays(-3);
            double expected = 3;

            // Act
            var actual = ticket.DaysSinceCreation(ticket.CreationDate);
            
            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact] // Checks if invalid input passes for DaysSinceCreation()
        public void DaysSinceCreation_Invalid()
        {
            // Arrange
            Ticket ticket = new Ticket();
            ticket.CreationDate = DateTime.Now.AddDays(-2);
            double expected = 3;

            // Act
            var actual = ticket.DaysSinceCreation(ticket.CreationDate);
            
            // Assert
            Assert.NotEqual(expected, actual);
        }
    }
}
