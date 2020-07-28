using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace itmanager.Models
{
    public class TicketSession
    {

        private const string TicketsKey = "mytickets";
        private const string CountKey = "ticketcount";
        private const string StoreKey = "store";

        private ISession session { get; set; }
        public TicketSession(ISession session)
        {
            this.session = session;
        }

        public void SetMyTickets(List<Ticket> tickets)
        {
            session.SetObject(TicketsKey, tickets);
            session.SetInt32(CountKey, tickets.Count);
        }

        public List<Ticket> GetMyTickets() =>
            session.GetObject<List<Ticket>>(TicketsKey) ?? new List<Ticket>();
        public int? GetMyTicketCount() => session.GetInt32(CountKey);

        public void SetActiveStore(string activeStore) =>
            session.SetString(StoreKey, activeStore);
        public string GetActiveStore() => session.GetString(StoreKey);

        public void RemoveMyTickets()
        {
            session.Remove(TicketsKey);
            session.Remove(CountKey);
        }
    }
}
