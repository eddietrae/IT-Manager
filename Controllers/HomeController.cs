using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using itmanager.Models;

namespace itmanager.Controllers
{
    public class HomeController : Controller
    {
        private TicketContext context;
        public HomeController(TicketContext ctx) => context = ctx;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Ticket()
        {
            return View();
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
