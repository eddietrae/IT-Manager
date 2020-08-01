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
using Moq;

namespace itmanager.Controllers
{
    // Controls homepage and all ticket pages
    public class HomeController : Controller
    {
        private TicketContext context;

        public HomeController(TicketContext ctx) => context = ctx;

        [HttpGet] // Index is currently a log in page since you need to be signed in to do anything in tickets as is
        public IActionResult Index(string returnURL = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnURL };
            return View(model);
        }
        // Must be logged in to access anything involving tickets
        [Authorize]
        public IActionResult Ticket(string id)
        {
            // Load current filters and data needed for filter drop downs in ViewBag
            var filters = new Filters(id);
            ViewBag.Filters = filters;
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Severities = context.Severities.ToList();
            ViewBag.StoreId = (from s in context.Users.AsNoTracking()
                              where s.UserName == User.Identity.Name
                              select s.StoreId).Single();
            

            // Get Ticket objects from database based on current filters
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
        [HttpPost] // Uses filter class to sort the table
        public IActionResult Filter(string[] filter)
        {
            string id = string.Join('-', filter);
            return RedirectToAction("Ticket", new { ID = id });
        }

        [Authorize] // Add function for tickets. Passes an empty ticket to the edit view
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Severities = context.Severities.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Stores = context.Stores.ToList();
            return View("Edit", new Ticket());
        }

        [Authorize] // Edit function for tickets. Takes the ticket id and passses that ticket information to view
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.Severities = context.Severities.ToList();
            ViewBag.Statuses = context.Statuses.ToList();
            ViewBag.Stores = context.Stores.ToList();
            var ticket = context.Tickets.Find(id);
            ViewBag.Date = ticket.CreationDate;
            return View(ticket);
        }

        [Authorize]
        [HttpPost] // Takes ticket and validates that it is okay to post.
        public IActionResult Edit(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                if (ticket.TicketId == 0)
                {
                    context.Tickets.Add(ticket); // Adds if new ticket
                }
                else
                    context.Tickets.Update(ticket); // Updates if existing
                context.SaveChanges();
                return RedirectToAction("Ticket", "Home");
            }
            else // Model state wasnt valid so show the view again
            {
                ViewBag.Action = (ticket.TicketId == 0) ? "Add" : "Edit";
                ViewBag.Severities = context.Severities.ToList();
                ViewBag.Statuses = context.Statuses.ToList();
                ViewBag.Stores = context.Stores.ToList();
                ViewBag.Date = ticket.CreationDate;
                return View(ticket);
            }
        }

        [Authorize]
        [HttpGet] // Takes ticket id and passes it to delete view
        public IActionResult Delete(int id)
        {
            var ticket = context.Tickets.Find(id);
            return View(ticket);
        }

        [Authorize]
        [HttpPost] // If user does want to remove the ticket displayed it removes the ticket and returns to tickets
        public IActionResult Delete(Ticket ticket)
        {
            context.Tickets.Remove(ticket);
            context.SaveChanges();
            return RedirectToAction("Ticket", "Home");
        }

        [Route("[controller]/Details/{id?}")]
        [HttpGet] // Details page for each ticket based on ticket id
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
        [HttpPost] // Adds ticket to watch list
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

            TempData["message"] = $"{model.Ticket.ShortDescription} added to your watch list";

            return RedirectToAction("Ticket", "Home",
                new
                {
                    ActiveSportType = session.GetActiveStore(),
                });
        }
    }
}
