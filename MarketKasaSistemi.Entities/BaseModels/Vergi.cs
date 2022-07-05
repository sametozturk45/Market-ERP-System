using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace MarketKasaSistemi.Entities
{
    public class Vergi : IModel
    {
        public int Id { get; set; }
        public int VergiMiktar { get; set; }

        public SqlParameter GetIdParameter()
        {
            return new SqlParameter("VergiId", this.Id);
        }

        public List<SqlParameter> GetInsertParameters()
        {
            return new List<SqlParameter> { 
                new SqlParameter("VergiMiktar", this.VergiMiktar)
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
            this.Id = Convert.ToInt32(reader["VergiId"]);
            this.VergiMiktar = Convert.ToInt32(reader["VergiMiktar"]);
        }
    }
}
