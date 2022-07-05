using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;

namespace MarketKasaSistemi.Entities
{
    public class Fis : IModel
    {
        [Browsable(false)]
        public int Id { get; set; }
        public DateTime FisTarih { get; set; }
        public OdemeTip OdemeTip { get; set; }
        public Personel Personel { get; set; }

        public SqlParameter GetIdParameter()
        {
            return new SqlParameter("FisId", this.Id);
        }

        public List<SqlParameter> GetInsertParameters()
        {
            return new List<SqlParameter> { 
                new SqlParameter("FisTarih", this.FisTarih),
                new SqlParameter("OdemeTipId", this.OdemeTip.Id),
                new SqlParameter("PersonelId", this.Personel.Id),
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
            this.Id = Convert.ToInt32(reader["FisId"]);
            this.FisTarih = Convert.ToDateTime(reader["FisTarih"]);

            this.OdemeTip = new OdemeTip();
            this.OdemeTip.ReadItem(reader);

            this.Personel = new Personel();
            this.Personel.ReadItem(reader);
        }
    }
}
