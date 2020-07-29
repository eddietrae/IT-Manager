using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace itmanager.Models
{
    public class Filters
    {
        public Filters(string filterstring)
        {
            // setting default filters and the break out of the two filters
            FilterString = filterstring ?? "all-all";
            string[] filters = FilterString.Split('-');
            SeverityId = filters[0];
            StatusId = filters[1];
        }
        // getters
        public string FilterString { get; }
        public string SeverityId { get; }
        public string StatusId { get; }

        // if either filter not set - default to all
        public bool HasSeverity => SeverityId.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";

    }
}