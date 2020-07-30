using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using itmanager.Models;

namespace itmanager.Controllers
{
    public class WatchController : Controller
    {
        public ViewResult Index()
        {
            var session = new TicketSession(HttpContext.Session);
            var model = new TicketListViewModel
            {
                ActiveStore = session.GetActiveStore(),
                Tickets = session.GetMyTickets()
            };
            return View(model);
        }
        
        [HttpPost]
        public RedirectToActionResult Delete()
        {
            var session = new TicketSession(HttpContext.Session);
            var cookies = new TicketCookies(Response.Cookies);

            session.RemoveMyTickets();
            cookies.RemoveMyTicketIds();

            TempData["message"] = "Watched tickets cleared";

            return RedirectToAction("Ticket", "Home",
                new
                {
                    ActiveStore = session.GetActiveStore(),
                });
        }
    }
}