using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.DataAccess.Repositories
{
    public class SatisRepository : ARepository<Satis>, IDisposable
    {
        public SatisRepository(DBContext context) : base(context) { }

        public override object Add(Satis item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPSatisAdd", item.GetInsertParameters()))
            {
                return context.ExecuteScalar(cmd);
            }
        }

        public override Satis GetItem(object value)
        {
            using (SqlCommand cmd = context.CreateCommand("SPSatisGetById", new SqlParameter("@SatisId", value)))
            {
                return context.GetItem<Satis>(cmd);
            }
        }

        public override int Remove(Satis item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPSatisDelete", item.GetIdParameter()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public override List<Satis> ToList()
        {
            using (SqlCommand cmd = context.CreateCommand("SPSatisGetAll"))
            {
                return context.ToList<Satis>(cmd);
            }
        }

        public List<Satis> AllSatisByFisId(int FisId)
        {
            using (SqlCommand cmd = context.CreateCommand("SPSatisGetAllByFisId", new SqlParameter("FisId", FisId)))
            {
                return context.ToList<Satis>(cmd);
            }
        }

        public List<ZRaporu> GetZReport()
        {
            using (SqlCommand cmd = context.CreateCommand("SELECT * FROM VwZReport", System.Data.CommandType.Text))
            {
                return context.ToList<ZRaporu>(cmd);
            }
        }

        public override int Update(Satis item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPSatisUpdate", item.GetUpdateParameters()))
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
