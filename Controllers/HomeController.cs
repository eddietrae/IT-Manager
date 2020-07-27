using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using itmanager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace itmanager.Controllers
{

    public class HomeController : Controller
    {
        private TicketContext context;
        public HomeController(TicketContext ctx) => context = ctx;

        [HttpGet]
        public IActionResult Index(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }
        [Authorize]
        public IActionResult Ticket(string id)
        {
            // load current filters and data needed for filter drop downs in ViewBag
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Severities = context.Severities.ToList();

            // get ToDo objects from database based on current filters
            IQueryable<Ticket> query = context.Tickets.Include(t => t.Status);
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }
            if (filters.HasSeverity)
            {
                query = query.Where(t => t.SeverityId == filters.SeverityId);
            }
            var ticket = query.OrderBy(t => t.StatusId).ToList();
            return View(ticket);
        }
        [HttpPost] // uses filter class to sort the table
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Ticket", new { ID = id });
        }
        [Authorize]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Severities = context.Severities.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Employees = context.Employees.ToList();
            ViewBag.Stores = context.Stores.ToList();
            return View("Edit", new Ticket());
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Severities = context.Severities.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Employees = context.Employees.ToList();
            ViewBag.Stores = context.Stores.ToList();
            var ticket = context.Tickets.Find(id);
            return View(ticket);
        }
        [HttpPost]
        public IActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                if (ticket.TicketId == 0)
                {
                    context.Tickets.Add(ticket);
                }
                else
                    context.Tickets.Update(ticket);
                context.SaveChanges();
                return RedirectToAction("Ticket", "Home");
            }
            else
            {
                ViewBag.Action = (ticket.TicketId == 0) ? "Add" : "Edit";
                return View(ticket);
            }
        }
        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ticket = context.Tickets.Find(id);
            return View(ticket);
        }
        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Delete(Ticket ticket)
        {
            context.Tickets.Remove(ticket);
            context.SaveChanges();
            return RedirectToAction("Ticket", "Home");
        }
    }
}
