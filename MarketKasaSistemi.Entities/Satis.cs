using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MarketKasaSistemi.Entities
{
    public class Satis : IModel
    {
        private Urun _urun = new Urun();

        [Browsable(false)]
        public int Id { get; set; }

        [Browsable(false)]
        public Fis Fis { get; set; }

        [Browsable(false)]
        public Urun Urun { get { return _urun; } set { _urun = value; } }
        public int UrunBarkod { get { return Urun.Id; } set { Urun.Id = value; } }
        public string UrunAd { get { return Urun.UrunAd; } set { Urun.UrunAd = value; } }
        public int SatisAdet { get; set; }
        public decimal ToplamFiyat { get { return SatisAdet * Urun.UrunFiyat; } }

        [Browsable(false)]
        public decimal ToplamKdvliFiyat { get { return SatisAdet * (Urun.UrunFiyat + Urun.UrunFiyat * Urun.VergiMiktar / 100); } }

        [Browsable(false)]
        public decimal UrunFiyat { get { return Urun.UrunFiyat; } }

        public SqlParameter GetIdParameter()
        {
            return new SqlParameter("SatisId", this.Id);
        }

        public List<SqlParameter> GetInsertParameters()
        {
            return new List<SqlParameter> { 
                new SqlParameter("SatisAdet", this.SatisAdet),
                new SqlParameter("FisId", this.Fis.Id),
                new SqlParameter("UrunBarkod", this.Urun.Id),
            };
        }

        public List<SqlParameter> GetUpdateParameters()
        {
            List<SqlParameter> parameters = GetInsertParameters();
            parameters.Add(GetIdParameter());
            return parameters;
        }

        public void ReadItem(SqlDataReader reader)
        {
            this.Id = Convert.ToInt32(reader["SatisId"]);
            this.SatisAdet = Convert.ToInt32(reader["SatisAdet"]);

            this.Fis = new Fis();
            this.Fis.ReadItem(reader);

            this.Urun = new Urun();
            this.Urun.ReadItem(reader);
        }
    }
}
