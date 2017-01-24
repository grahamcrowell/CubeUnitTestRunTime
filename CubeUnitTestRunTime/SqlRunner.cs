using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CubeUnitTestRunTime
{
    public class SqlRunner
    {
        public string sql_code { get; set; }
        public string sql_server_name { get; set; }
        public IDataReader sql_data_reader { get; set; }
        public SqlConnection sql_connection { get; set; }
        public DataTable query_result_set { get; set; }
        public List<string> column_names { get; set; }

        public SqlRunner(string sql_code, string sql_server_name)
        {
            this.sql_code = sql_code;
            this.sql_server_name = sql_server_name;
            string sqlConnectionString = $"Data Source={sql_server_name};Initial Catalog=master;Integrated Security=SSPI";
            sql_connection = new SqlConnection(sqlConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = sql_code;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sql_connection;

            sql_connection.Open();

            reader = cmd.ExecuteReader();
            sql_data_reader = reader as IDataReader;
            // Data is accessible through the DataReader object here.

            column_names = new List<string>(reader.FieldCount);
            for (int i = 0; i < reader.FieldCount; i++)
            {
                column_names[i] = reader.GetName(i);
            }

        }
        public void clean()
        {
            sql_connection.Close();
        }

    }
}
