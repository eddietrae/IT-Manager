using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace itmanager.Models
{
    public class TicketSession
    {
        // constants for keys
        private const string TicketsKey = "mytickets";
        private const string CountKey = "ticketcount";
        private const string StoreKey = "store";


        private ISession session { get; set; }
        // receive ISession argument and store it
        public TicketSession(ISession session)
        {
            this.session = session;
        }

        //set session method for tickets and count
        public void SetMyTickets(List<Ticket> tickets)
        {
            session.SetObject(TicketsKey, tickets);
            session.SetInt32(CountKey, tickets.Count);
        }

        // get session method for tickets
        public List<Ticket> GetMyTickets() =>
            session.GetObject<List<Ticket>>(TicketsKey) ?? new List<Ticket>();
        // get session method for count
        public int? GetMyTicketCount() => session.GetInt32(CountKey);

        // set session method for store
        public void SetActiveStore(string activeStore) =>
            session.SetString(StoreKey, activeStore);
        // get session method for store
        public string GetActiveStore() => session.GetString(StoreKey);

        // method for deleting all tickets from watch list
        public void RemoveMyTickets()
        {
            session.Remove(TicketsKey);
            session.Remove(CountKey);
        }
    }
}
