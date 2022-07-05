using MarketKasaSistemi.Entities;
using MarketKasaSistemi.DataAccess;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.DataAccess.Repositories
{
    public class FisRepository : ARepository<Fis>, IDisposable
    {
        public FisRepository(DBContext context) : base(context) { }

        public override object Add(Fis item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPFisAdd", item.GetInsertParameters()))
            {
                return context.ExecuteScalar(cmd);
            }
        }

        public override Fis GetItem(object value)
        {
            using (SqlCommand cmd = context.CreateCommand("SPFisGetById", new SqlParameter("@FisId", value)))
            {
                return context.GetItem<Fis>(cmd);
            }
        }

        public override int Remove(Fis item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPFisDelete", item.GetIdParameter()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public override List<Fis> ToList()
        {
            using (SqlCommand cmd = context.CreateCommand("SPFisGetAll"))
            {
                return context.ToList<Fis>(cmd);
            }
        }

        public override int Update(Fis item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPFisUpdate", item.GetUpdateParameters()))
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
