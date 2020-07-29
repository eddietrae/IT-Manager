using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itmanager.Models
{
    public class Nav
    {
        // set up to know which page is currently active in Nav bar
        public static string Active(string value, string current) =>
            (value == current) ? "active" : "";
        public static string Active(int value, int current) =>
            (value == current) ? "active" : "";
    }
}
