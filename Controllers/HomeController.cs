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
            ViewBag.StoreId = (from s in context.Users.AsNoTracking()
                              where s.UserName == User.Identity.Name
                              select s.StoreId).Single();
            

            // get Ticket objects from database based on current filters
            IQueryable < Ticket > query = context.Tickets.Include(t => t.Status);
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

        [Authorize]
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
            ViewBag.Stores = context.Stores.ToList();
            var ticket = context.Tickets.Find(id);
            return View(ticket);
        }

        [Authorize]
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

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var ticket = context.Tickets.Find(id);
            return View(ticket);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Delete(Ticket ticket)
        {
            context.Tickets.Remove(ticket);
            context.SaveChanges();
            return RedirectToAction("Ticket", "Home");
        }

        [Route("[controller]/Details/{id?}")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var session = new TicketSession(HttpContext.Session);
            var model = new TicketViewModel
            {
                Ticket = context.Tickets
                    .Include(t => t.Store)
                    .FirstOrDefault(t => t.TicketId == id),
                ActiveStore = session.GetActiveStore(),
            };
            return View(model);
        }

        [Route("[controller]/Watches/{id?}")]
        [HttpPost]
        public RedirectToActionResult AddWatch(TicketViewModel model)
        {
            model.Ticket = context.Tickets
                .Include(t => t.Store)
                .Where(t => t.TicketId == model.Ticket.TicketId)
                .FirstOrDefault();

            var session = new TicketSession(HttpContext.Session);
            var tickets = session.GetMyTickets();
            tickets.Add(model.Ticket);
            session.SetMyTickets(tickets);

            var cookies = new TicketCookies(Response.Cookies);
            cookies.SetMyTicketIds(tickets);

            TempData["message"] = $"{model.Ticket.ShortDescription} added to your favorites";

            return RedirectToAction("Ticket", "Home",
                new
                {
                    ActiveSportType = session.GetActiveStore(),
                });
        }
    }
}
