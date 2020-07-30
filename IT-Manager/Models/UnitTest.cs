using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using itmanager.Models;
using itmanager;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace itmanager.Models
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

            Assert.Equals(expected, actual);
        }

        [Fact]
        public void DaysSinceCreation_Invalid()
        {
            Ticket ticket = new Ticket();
            ticket.CreationDate = DateTime.Now.AddDays(-2);


            var actual = ticket.DaysSinceCreation(ticket.CreationDate);
            double expected = 3;

            Assert.Equals(expected, actual);
        }
    }
}
