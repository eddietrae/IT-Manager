using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace itmanager.Models
{
    public interface IRepository<T> where T : class
    {
        // set up of IRepository class to be able to use for Ticket view
        IEnumerable<T> List(QueryOptions<T> options);

        T Get(int id);
        T Get(QueryOptions<T> options);

        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}
