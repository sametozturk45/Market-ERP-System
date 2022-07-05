using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.DataAccess.Repositories
{
    public class UrunRepository : ARepository<Urun>, IDisposable
    {
        public UrunRepository(DBContext context) : base(context) { }

        public override object Add(Urun item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPUrunAdd", item.GetInsertParameters()))
            {
                return context.ExecuteScalar(cmd);
            }
        }

        public override Urun GetItem(object value)
        {
            using (SqlCommand cmd = context.CreateCommand("SPUrunGetById", new SqlParameter("@UrunBarkod", value)))
            {
                return context.GetItem<Urun>(cmd);
            }
        }

        public override int Remove(Urun item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPUrunDelete", item.GetIdParameter()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public override List<Urun> ToList()
        {
            using (SqlCommand cmd = context.CreateCommand("SPUrunGetAll"))
            {
                return context.ToList<Urun>(cmd);
            }
        }

        public override int Update(Urun item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPUrunUpdate", item.GetUpdateParameters()))
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
