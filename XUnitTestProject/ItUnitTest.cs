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
        [Fact]
        public void DaysSinceCreation_Valid()
        {
            Ticket ticket = new Ticket();
            ticket.CreationDate = DateTime.Now.AddDays(-3);


            var actual = ticket.DaysSinceCreation(ticket.CreationDate);
            double expected = 3;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void DaysSinceCreation_Invalid()
        {
            Ticket ticket = new Ticket();
            ticket.CreationDate = DateTime.Now.AddDays(-2);


            var actual = ticket.DaysSinceCreation(ticket.CreationDate);
            double expected = 3;

            Assert.NotEqual(expected, actual);
        }
    }
}
