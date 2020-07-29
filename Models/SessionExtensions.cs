using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;

namespace itmanager.Models
{
    public static class SessionExtensions
    {
        // method to store an object of T type in session state object
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        // method to retrieve object of T type from session state object
        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return (string.IsNullOrEmpty(value)) ? default(T) :
                JsonConvert.DeserializeObject<T>(value);
        }
    }
}
