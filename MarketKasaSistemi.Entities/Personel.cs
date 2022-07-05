using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MarketKasaSistemi.Entities
{
    public class Personel : IModel
    {
        public int Id { get; set; }
        public string PersonelAd { get; set; }
        public string PersonelSoyad { get; set; }
        [Browsable(false)]
        public string PersonelTamAd { get { return PersonelAd + " " + PersonelSoyad; } }
        public DateTime PersonelBaslangicTarih { get; set; }
        [Browsable(false)]
        public PersonelTip PersonelTip { get; set; }
        public string PersonelTipAd { get { return PersonelTip.PersonelTipAd; } }

        public SqlParameter GetIdParameter()
        {
            return new SqlParameter("PersonelId", this.Id);
        }

        public List<SqlParameter> GetInsertParameters()
        {
            return new List<SqlParameter> { 
                new SqlParameter("PersonelAd", this.PersonelAd),
                new SqlParameter("PersonelSoyad", this.PersonelSoyad),
                new SqlParameter("PersonelBaslangicTarih", this.PersonelBaslangicTarih),
                new SqlParameter("PersonelTipId", this.PersonelTip.Id),
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
            this.Id = Convert.ToInt32(reader["PersonelId"]);
            this.PersonelAd = reader["PersonelAd"].ToString();
            this.PersonelSoyad = reader["PersonelSoyad"].ToString();
            this.PersonelBaslangicTarih = Convert.ToDateTime(reader["PersonelBaslangicTarih"]);

            this.PersonelTip = new PersonelTip();
            this.PersonelTip.ReadItem(reader);
        }
    }
}
