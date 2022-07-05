using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MarketKasaSistemi.Entities
{
    public class Kullanici : IModel
    {
        public int Id { get; set; }
        [DisplayName("Kullanıcı Adı")]
        public string KullaniciAd { get; set; }

        [Browsable(false),DisplayName("Şifre")]
        public string KullaniciSifre { get; set; }

        [Browsable(false)]
        public Personel Personel { get; set; }
        public string PersonelAd { get { return Personel.PersonelAd; } }

        public SqlParameter GetIdParameter()
        {
            return new SqlParameter("KullaniciId", this.Id);
        }

        public List<SqlParameter> GetInsertParameters()
        {
            return new List<SqlParameter> { 
                new SqlParameter("KullaniciAd", this.KullaniciAd),
                new SqlParameter("KullaniciSifre", this.KullaniciSifre),
                new SqlParameter("PersonelId", this.Personel.Id),
            };
        }

        public List<SqlParameter> GetUpdateParameters()
        {
            List<SqlParameter> parameters = GetInsertParameters();
            parameters.Add(GetIdParameter());
            return parameters;
        }

        public List<SqlParameter> GetLoginParameters()
        {
            return new List<SqlParameter> {
                new SqlParameter("KullaniciAd", this.KullaniciAd),
                new SqlParameter("KullaniciSifre", this.KullaniciSifre),
            };
        }

        public void ReadItem(SqlDataReader reader)
        {
            this.Id = Convert.ToInt32(reader["KullaniciId"]);
            this.KullaniciAd = reader["KullaniciAd"].ToString();
            this.KullaniciSifre = reader["KullaniciSifre"].ToString();

            this.Personel = new Personel();
            this.Personel.ReadItem(reader);
        }
    }
}
