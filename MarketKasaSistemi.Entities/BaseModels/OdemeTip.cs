using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.Entities
{
    public class OdemeTip : IModel
    {
        public int Id { get; set; }
        public string OdemeTipAd { get; set; }

        public SqlParameter GetIdParameter()
        {
            return new SqlParameter("OdemeTipId", this.Id);
        }

        public List<SqlParameter> GetInsertParameters()
        {
            return new List<SqlParameter> {
                new SqlParameter("OdemeTipAd", this.OdemeTipAd)
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
            this.Id = Convert.ToInt32(reader["OdemeTipId"]);
            this.OdemeTipAd = reader["OdemeTipAd"].ToString();
        }
    }
}
