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

        public IActionResult Ticket(string id)
        {
            // load current filters and data needed for filter drop downs in ViewBag
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = context.Statuses.ToList();

            // get ToDo objects from database based on current filters
            IQueryable<Ticket> query = context.Tickets.Include(t => t.Status);
            if (filters.HasStatus)
            {
                query = query.Where(t => t.StatusId == filters.StatusId);
            }
            var tasks = query.OrderBy(t => t.StatusId).ToList();
            return View(tasks);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Severities = context.Severities.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Employees = context.Employees.ToList();
            ViewBag.Stores = context.Stores.ToList();
            return View("Edit", new Ticket());
        }

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

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ticket = context.Tickets.Find(id);
            return View(ticket);
        }

        [HttpPost]
        public IActionResult Delete(Ticket ticket)
        {
            context.Tickets.Remove(ticket);
            context.SaveChanges();
            return RedirectToAction("Ticket", "Home");
        }
    }
}
