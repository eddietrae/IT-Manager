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
        // constant for accessing ticket ids
        private const string TicketsKey = "mytickets";
        // constant for delimiting to separate ticket ids
        private const string Delimiter = "-";

        private IRequestCookieCollection requestCookies { get; set; }
        private IResponseCookies responseCookies { get; set; }
        // receives and stores collection of request cookies
        public TicketCookies(IRequestCookieCollection cookies)
        {
            requestCookies = cookies;
        }
        // receives and stores collection of response cookies
        public TicketCookies(IResponseCookies cookies)
        {
            responseCookies = cookies;
        }

        // set up list of ticket objects into a single delimited string, set expiration info, 
        // remove old cookie, create new
        public void SetMyTicketIds(List<Ticket> mytickets)
        {
            List<int> ids = mytickets.Select(t => t.TicketId).ToList();
            string idsString = String.Join(Delimiter, ids);
            CookieOptions options = new CookieOptions { Expires = DateTime.Now.AddDays(30) };
            RemoveMyTicketIds();    
            responseCookies.Append(TicketsKey, idsString, options);
        }

        // retrieves persistent cookie, splits ids if not empty or null
        public string[] GetMyTicketIds()
        {
            string cookie = requestCookies[TicketsKey];
            if (string.IsNullOrEmpty(cookie))
                return new string[] { };   // empty string array
            else
                return cookie.Split(Delimiter);
        }

        // delete persistent cookie
        public void RemoveMyTicketIds()
        {
            responseCookies.Delete(TicketsKey);
        }
    }
}