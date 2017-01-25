using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace CubeUnitTestRunTime
{
    public class SqlRunner : TestQuery
    {
        public string query_source_code { get; set; }
        public List<string> column_names { get; private set; }
        public DataTable query_result_set { get; private set; }
        public string server_name { get; private set; }


        private SqlConnection sql_connection { get; set; }
        public SqlRunner(string sql_code, string sql_server_name)
        {
            this.query_source_code = sql_code;
            this.server_name = sql_server_name;
            string sqlConnectionString = $"Data Source={sql_server_name};Initial Catalog=master;Integrated Security=SSPI";
            sql_connection = new SqlConnection(sqlConnectionString);
            sql_connection.Open();


            SqlDataAdapter adapter = new SqlDataAdapter(sql_code, sql_connection);
            query_result_set = new DataTable();
            adapter.Fill(query_result_set);

            column_names = new List<string>(query_result_set.Columns.Count);
            foreach (DataColumn column in query_result_set.Columns)
            {
                column_names.Add(column.ColumnName);
            }
            sql_connection.Close();
        }
    }
}
