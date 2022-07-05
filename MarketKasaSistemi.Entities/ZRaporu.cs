using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketKasaSistemi.Entities
{
    public class ZRaporu : IModel
    {
        [Browsable(false)]
        public int Id { get; set; }
        public int FisId { get; set; }
        public DateTime FisTarih { get; set; }
        public string OdemeTipAd { get; set; }
        public decimal ToplamFiyat { get; set; }

        public SqlParameter GetIdParameter()
        {
            throw new NotImplementedException();
        }

        public List<SqlParameter> GetInsertParameters()
        {
            throw new NotImplementedException();
        }

        public List<SqlParameter> GetUpdateParameters()
        {
            throw new NotImplementedException();
        }

        public void ReadItem(SqlDataReader reader)
        {
            this.FisId = Convert.ToInt32(reader["FisId"]);
            this.FisTarih = Convert.ToDateTime(reader["FisTarih"]);
            this.OdemeTipAd = reader["OdemeTipAd"].ToString();
            this.ToplamFiyat = Convert.ToDecimal(reader["ToplamFiyat"]);
        }
    }
}
