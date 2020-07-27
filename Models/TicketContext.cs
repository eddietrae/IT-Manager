﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace itmanager.Models
{
    public class TicketContext : IdentityDbContext<User>
    {
        public TicketContext(DbContextOptions<TicketContext> options)
            : base(options)
        { }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Severity> Severities { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Store> Stores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlServer(@"Server=tcp:cis174pford.database.windows.net,1433;
                        Initial Catalog=CIS174;Persist Security Info=False;User ID=cis174;Password=Gemini99$;MultipleActiveResultSets=False;Encrypt=True;
                        TrustServerCertificate=False;Connection Timeout=30;");


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Severity>().HasData(
                new Severity { SeverityId = "1", Name = "Low" },
                new Severity { SeverityId = "2", Name = "Medium" },
                new Severity { SeverityId = "3", Name = "High" },
                new Severity { SeverityId = "4", Name = "Emergency" }
                );
            modelBuilder.Entity<Status>().HasData(
                new Status { StatusId = "1", Name = "In Queue"},
                new Status { StatusId = "2", Name = "In Progress"},
                new Status { StatusId = "3", Name = "Complete" }
                );
            modelBuilder.Entity<Store>().HasData(
                new Store { StoreId = 1, StoreAlias = "1375", StreetAddress = "2900 Devils Glen Road", City = "Bettendorf", State = "IA", Zip = "52722" },
                new Store { StoreId = 2, StoreAlias = "5035", StreetAddress = "1307 East North Avenue", City = "Belton", State = "MO", Zip = "64012" },
                new Store { StoreId = 3, StoreAlias = "1889", StreetAddress = "1307 18th Ave NW", City = "Austin", State = "MN", Zip = "55912" },
                new Store { StoreId = 4, StoreAlias = "4205", StreetAddress = "2930 18th Avenue", City = "Rock Island", State = "IL", Zip = "61201" },
                new Store { StoreId = 5, StoreAlias = "1601", StreetAddress = "1700 Valley West Drive", City = "West Des Moines", State = "IA", Zip = "50266" }
                );
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 5001, FirstName = "Paul", LastName = "Ford", Authority = "Employee", Password = "Password", StoreId = 1},
                new Employee { EmployeeId = 81112, FirstName = "Trevor", LastName = "Miller", Authority = "Admin", Password = "Password", StoreId = 2},
                new Employee { EmployeeId = 82345, FirstName = "Shane", LastName = "Blume", Authority = "Employee", Password = "Password", StoreId = 3},
                new Employee { EmployeeId = 88812, FirstName = "Trae", LastName = "Eddie", Authority = "Admin", Password = "Password", StoreId = 4},
                new Employee { EmployeeId = 86421, FirstName = "Hillary", LastName = "Murphy", Authority = "Admin", Password = "Password", StoreId = 5}
                );
            modelBuilder.Entity<Ticket>().HasData(
                new Ticket
                {
                    TicketId = 1,
                    ShortDescription = "First Ticket",
                    DetailedDescription = "First upload of a ticket to make sure we can connect to a database",
                    EmployeeId = 5001,
                    StoreId = 1,
                    StatusId = "1",
                    SeverityId = "1"
                },
                new Ticket
                {
                    TicketId = 2,
                    ShortDescription = "Second Ticket",
                    DetailedDescription = "My register is broken..... mehhhhhhhhhhhhhhh",
                    EmployeeId = 81112,
                    StoreId = 2,
                    StatusId = "1",
                    SeverityId = "2"
                },
                new Ticket
                {
                    TicketId = 3,
                    ShortDescription = "Third Ticket",
                    DetailedDescription = "This register is causing us issues.",
                    EmployeeId = 82345,
                    StoreId = 3,
                    StatusId = "1",
                    SeverityId = "3"
                },
                new Ticket
                {
                    TicketId = 4,
                    ShortDescription = "Fouth Ticket",
                    DetailedDescription = "Nothing like pharmacy screwing up....... blah",
                    EmployeeId = 88812,
                    StoreId = 4,
                    StatusId = "1",
                    SeverityId = "1"
                },
                new Ticket
                {
                    TicketId = 5,
                    ShortDescription = "Fifth Ticket",
                    DetailedDescription = "The whole store is down",
                    EmployeeId = 86421,
                    StoreId = 5,
                    StatusId = "1",
                    SeverityId = "4"
                }
            );
        }
    }
}
