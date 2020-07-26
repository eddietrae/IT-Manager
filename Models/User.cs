using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace itmanager.Models
{
    public class User : IdentityUser
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public string StoreId { get; set; }

        public Store Store { get; set; }

        [NotMapped]
        public IList<string> RoleNames { get; set; }

    }
}
