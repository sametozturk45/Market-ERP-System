using MarketKasaSistemi.DataAccess;
using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.DataAccess.Repositories
{
    public class KullaniciRepository : ARepository<Kullanici>, IDisposable
    {
        public KullaniciRepository(DBContext context) : base(context) { }

        public override object Add(Kullanici item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPKullaniciAdd", item.GetInsertParameters()))
            {
                return context.ExecuteScalar(cmd);
            }
        }

        public override Kullanici GetItem(object value)
        {
            using (
                SqlCommand cmd =
                value.GetType() == typeof(int)
                ? context.CreateCommand("SPKullaniciGetById", new SqlParameter("@KullaniciId", value))
                : context.CreateCommand("SPKullaniciGetByAd", new SqlParameter("@KullaniciAd", value))
                )
            {
                return context.GetItem<Kullanici>(cmd);
            }
        }

        public override int Remove(Kullanici item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPKullaniciDelete", item.GetIdParameter()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public override List<Kullanici> ToList()
        {
            using (SqlCommand cmd = context.CreateCommand("SPKullaniciGetAll"))
            {
                return context.ToList<Kullanici>(cmd);
            }
        }

        public override int Update(Kullanici item)
        {
            using (SqlCommand cmd = context.CreateCommand("SPKullaniciUpdate", item.GetUpdateParameters()))
            {
                return context.ExecuteNonQuery(cmd);
            }
        }

        public bool Login(string kullaniciAd, string kullaniciSifre)
        {
            using (SqlCommand cmd = context.CreateCommand("SPKullaniciLogin"))
            {
                cmd.Parameters.AddWithValue("KullaniciAd", kullaniciAd);
                cmd.Parameters.AddWithValue("KullaniciSifre", kullaniciSifre);
                return Convert.ToBoolean(context.ExecuteScalar(cmd));
            }
        }

        public void Dispose()
        {
            context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
