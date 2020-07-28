using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace itmanager.Models
{
    public class TicketCookies
    {
        private const string TicketsKey = "mytickets";
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }
        public TicketCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }
        public TicketCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        public void SetMyTicketIds(List<Ticket> mytickets)
        {
            List<int> ids = mytickets.Select(t => t.TicketId).ToList();
            string idsString = String.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(30) };
            RemoveMyTicketIds();     // delete old cookie first
            responseCookies.Append(TicketsKey, idsString, options);
        }

        public string[] GetMyTicketIds()
        {
            string cookie = requestCookies[TicketsKey];
            if (string.IsNullOrEmpty(cookie))
                return new string[] { };   // empty string array
            else
                return cookie.Split(Delimiter);
        }

        public void RemoveMyTicketIds()
        {
            responseCookies.Delete(TicketsKey);
        }
    }
}