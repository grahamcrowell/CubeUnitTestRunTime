using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
using System.Data;

namespace CubeUnitTestRunTime
{
	public class MdxRunner
	{
        public string query_source_code { get; set; }
        public List<string> column_names { get; private set; }
        public DataTable query_result_set { get; private set; }
        public string server_name { get; private set; }


        private string database_catalog_name { get; set; }
		public MdxRunner(string mdx_code, string ssas_server_name, string database_catalog_name)
		{
            query_source_code = mdx_code;
			server_name = ssas_server_name;
			this.database_catalog_name = database_catalog_name;

			string connection_string = $"Data source={server_name};Catalog={this.database_catalog_name}";
			connection_string = @"Data Source=STDSDB004\tabular;Catalog=ReferralWaitTimeCube";
            //TODO populate column_names

            AdomdConnection ado_cnx = new AdomdConnection(connection_string);
            ado_cnx.Open();
            
            AdomdDataAdapter currentDataAdapter = new AdomdDataAdapter(query_source_code, ado_cnx);
			query_result_set = new DataTable();
			currentDataAdapter.Fill(query_result_set);

            column_names = new List<string>(query_result_set.Columns.Count);
            foreach (DataColumn column in query_result_set.Columns)
            {
                if (column.ColumnName.Contains("[Measures]."))
                {
                    column_names.Add(column.ColumnName.Split('.')[1].Replace("[", "").Replace("]", ""));
                }
                else
                {
                    column_names.Add(column.ColumnName.Split('.')[2].Replace("[", "").Replace("]", ""));
                }

            }
            ado_cnx.Close();
        }


        public static string make_mdx_runner(SqlRunner sql_runner, string ssas_server_name, string database_catalog_name)
        {
            StringBuilder str_builder = new StringBuilder();
            // assume sql columns match cube measures/dims
            // assume 1st column name is measure name

            string mdx = string.Format(@"
SELECT 
	{{[Measures].[Unique Referrals]}} ON COLUMNS
	,{{[Fiscal Date].[Fiscal Year].ALLMEMBERS * [Referral Wait Time].[Paris Team].ALLMEMBERS}} ON ROWS
FROM [Model]
");
            Console.WriteLine(mdx);



            return str_builder.ToString();
        }




        //		public static DataTable tabularQueryExecute(string qry, AdomdConnection cnx)
        //		{
        //			AdomdDataAdapter currentDataAdapter = new AdomdDataAdapter(qry, cnx);
        //			DataTable tabularResults = new DataTable();
        //			currentDataAdapter.Fill(tabularResults);
        //			return tabularResults;
        //		}
        //		public static AdomdConnection GetCubeConnection()
        //		{
        //			AdomdConnection ado_cnx = new AdomdConnection(@"Data Source=VchDsWebT01\tabular;Catalog=ParisActivityCube");
        //			ado_cnx.Open();
        //			string qry;
        //			qry = @"
//        SELECT
//        	{[Measures].[Clients w Case Note]
//    }
//    ON COLUMNS,
//NON EMPTY[Date].[Fiscal Year Period].MEMBERS ON ROWS
//FROM[Model]
        //";
        //			DataTable dataTable = tabularQueryExecute(qry, ado_cnx);
        //			foreach (DataColumn column in dataTable.Columns)
        //			{
        //				Console.Write($"{column.ColumnName}\t");
        //			}
        //			Console.Write($"\n");

    //			foreach (DataRow row in dataTable.Rows)
    //			{
    //				foreach (object item in row.ItemArray)
    //				{
    //					Console.WriteLine($"{item.ToString()}");
    //				}


    //			}
    //			return ado_cnx;
    //		}
}
}