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
            FilterString = filterstring ?? "all-all";
            string[] filters = FilterString.Split('-');
            SeverityId = filters[0];
            StatusId = filters[1];
        }
        public string FilterString { get; }
        public string SeverityId { get; }
        public string StatusId { get; }

        public bool HasSeverity => SeverityId.ToLower() != "all";
        public bool HasStatus => StatusId.ToLower() != "all";

    }
}