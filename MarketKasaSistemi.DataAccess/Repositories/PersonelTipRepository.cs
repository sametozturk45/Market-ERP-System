using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.DataAccess.Repositories
{
    public class PersonelTipRepository : ARepository<PersonelTip>, IDisposable
    {
        public PersonelTipRepository(DBContext context) : base(context) { }

        public override object Add(PersonelTip item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPPersonelTipAdd", item.GetInsertParameters()))
            {
                return context.ExecuteScalar(cmd);
            }
        }

        public override PersonelTip GetItem(object value)
        {
            using (SqlCommand cmd = context.CreateCommand("SPPersonelTipGetById", new SqlParameter("@PersonelTipId", value)))
            {
                return context.GetItem<PersonelTip>(cmd);
            }
        }

        public override int Remove(PersonelTip item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPPersonelTipDelete", item.GetIdParameter()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public override List<PersonelTip> ToList()
        {
            using (SqlCommand cmd = context.CreateCommand("SPPersonelTipGetAll"))
            {
                return context.ToList<PersonelTip>(cmd);
            }
        }

        public override int Update(PersonelTip item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPPersonelTipUpdate", item.GetUpdateParameters()))
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
