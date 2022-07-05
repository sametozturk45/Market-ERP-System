using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.Entities
{
    public class PersonelTip : IModel
    {
        public int Id { get; set; }
        public string PersonelTipAd { get; set; }

        public SqlParameter GetIdParameter()
        {
            return new SqlParameter("PersonelTipId", this.Id);
        }

        public List<SqlParameter> GetInsertParameters()
        {
            return new List<SqlParameter> {
                new SqlParameter("PersonelTipAd", this.PersonelTipAd)
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
            this.Id = Convert.ToInt32(reader["PersonelTipId"]);
            this.PersonelTipAd = reader["PersonelTipAd"].ToString();
        }
    }
}
