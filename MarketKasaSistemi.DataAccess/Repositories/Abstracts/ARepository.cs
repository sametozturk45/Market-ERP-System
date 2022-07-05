using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using System.Collections.Generic;

namespace MarketKasaSistemi.DataAccess.Repositories
{
    public abstract class ARepository<T> where T : IModel
    {
        public readonly DBContext context;

        public ARepository(DBContext context)
        {
            this.context = context;
        }

        public abstract object Add(T item);
        public abstract T GetItem(object value);
        public abstract int Remove(T item);
        public abstract List<T> ToList();
        public abstract int Update(T item);
    }
}
