using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace itmanager.Models
{
    public class TicketListViewModel : TicketViewModel
    {
        public List<Ticket> Tickets { get; set; }

        private List<Store> stores;
        public List<Store> Stores
        {
            get => stores;
            set
            {
                stores = value;
                stores.Insert(0,
                    new Store { StoreId = 0, StoreAlias = "All", StreetAddress = "All", City = "All", State = "All", Zip = "All" });
            }
        }

        // methods to help view determine active link
        public string CheckActiveStore(string t) =>
            t.ToLower() == ActiveStore.ToLower() ? "active" : "";
    }
}
