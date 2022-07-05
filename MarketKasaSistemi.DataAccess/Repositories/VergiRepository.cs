using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.DataAccess.Repositories
{
    public class VergiRepository : ARepository<Vergi>, IDisposable
    {
        public VergiRepository(DBContext context) : base(context) { }

        public override object Add(Vergi item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPVergiAdd", item.GetInsertParameters()))
            {
                return context.ExecuteScalar(cmd);
            }
        }

        public override Vergi GetItem(object value)
        {
            using (SqlCommand cmd = context.CreateCommand("SPVergiGetById", new SqlParameter("@VergiId", value)))
            {
                return context.GetItem<Vergi>(cmd);
            }
        }

        public override int Remove(Vergi item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPVergiDelete", item.GetIdParameter()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public override List<Vergi> ToList()
        {
            using (SqlCommand cmd = context.CreateCommand("SPVergiGetAll"))
            {
                return context.ToList<Vergi>(cmd);
            }
        }

        public override int Update(Vergi item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPVergiUpdate", item.GetUpdateParameters()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public void Dispose()
        {
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
