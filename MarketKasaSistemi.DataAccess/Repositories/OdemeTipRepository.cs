using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.DataAccess.Repositories
{
    public class OdemeTipRepository : ARepository<OdemeTip>, IDisposable
    {
        public OdemeTipRepository(DBContext context) : base(context) { }

        public override object Add(OdemeTip item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPOdemeTipAdd", item.GetInsertParameters()))
            {
                return context.ExecuteScalar(cmd);
            }
        }

        public override OdemeTip GetItem(object value)
        {
            using (SqlCommand cmd = context.CreateCommand("SPOdemeTipGetById", new SqlParameter("@OdemeTipId", value)))
            {
                return context.GetItem<OdemeTip>(cmd);
            }
        }

        public override int Remove(OdemeTip item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPOdemeTipDelete", item.GetIdParameter()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public override List<OdemeTip> ToList()
        {
            using (SqlCommand cmd = context.CreateCommand("SPOdemeTipGetAll"))
            {
                return context.ToList<OdemeTip>(cmd);
            }
        }

        public override int Update(OdemeTip item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPOdemeTipUpdate", item.GetUpdateParameters()))
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
