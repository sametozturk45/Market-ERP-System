using MarketKasaSistemi.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketKasaSistemi.DataAccess
{
    public class DBContext : IDisposable
    {
        public SqlConnection Connection { get; private set; }

        public DBContext()
        {
            Connection = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\MarketDB.mdf;Integrated Security=SSPI;Connect Timeout=30");
        }

        public void OpenConnection()
        {
            try
            {
                if (Connection.State == ConnectionState.Closed)
                    Connection.Open();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (Connection.State == ConnectionState.Open)
                    Connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public SqlCommand CreateCommand(string commandText,
                                        CommandType commandType = CommandType.StoredProcedure)
        {
            SqlCommand cmd = Connection.CreateCommand();

            cmd.CommandText = commandText;
            cmd.CommandType = commandType;

            return cmd;
        }

        public SqlCommand CreateCommand(string commandText,
                                        SqlParameter parameter,
                                        CommandType commandType = CommandType.StoredProcedure)
        {
            SqlCommand cmd = CreateCommand(commandText, commandType);
            cmd.Parameters.Add(parameter);

            return cmd;
        }

        public SqlCommand CreateCommand(string commandText,
                                        List<SqlParameter> parameters,
                                        CommandType commandType = CommandType.StoredProcedure)
        {
            SqlCommand cmd = CreateCommand(commandText, commandType);

            cmd.Parameters.AddRange(parameters.ToArray());

            return cmd;
        }

        public object ExecuteScalar(SqlCommand cmd)
        {
            OpenConnection();
            object id = cmd.ExecuteScalar();
            CloseConnection();

            return id;
        }

        public int ExecuteNonQuery(SqlCommand cmd)
        {
            OpenConnection();
            int executedRows = cmd.ExecuteNonQuery();
            CloseConnection();

            return executedRows;
        }

        public T GetItem<T>(SqlCommand cmd) where T : IModel
        {
            T item = Activator.CreateInstance<T>();

            OpenConnection();
            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.HasRows && reader.Read())
            {
                item.ReadItem(reader);
            }

            CloseConnection();
            return item;
        }

        public List<T> ToList<T>(SqlCommand cmd) where T : IModel
        {
            List<T> items = new List<T>();

            OpenConnection();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                T item = Activator.CreateInstance<T>();

                if (reader.HasRows)
                {
                    item.ReadItem(reader);
                    items.Add(item);
                }
            }

            CloseConnection();
            return items;
        }

        public void Dispose()
        {
            Connection?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
