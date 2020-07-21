using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace itmanager.Models
{
    public class TicketSession
    {
        private const string EmpKey = "employee";

        private ISession session { get; set; }
        public TicketSession(ISession session)
        {
            this.session = session;
        }

        public void SetActiveEmployee(string activeEmployee) =>
            session.SetString(EmpKey, activeEmployee);
        public string GetActiveEmployee() => session.GetString(EmpKey);

        public void RemoveMyEmployees()
        {
            session.Remove(EmpKey);
        }
    }
}