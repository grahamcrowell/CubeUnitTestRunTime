using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeUnitTestRunTime
{
    class RunTime
    {

        public static bool sql_query_columns_exist_in_cube(SqlRunner sql_runner, MdxRunner mdx_runner)
        {
            return false;
        }
        public static string mdx_query(string database_name, string cube_name, string measure_name, List<string> dimension_names)
        {
            StringBuilder str_builder = new StringBuilder();
            return str_builder.ToString();

        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            SqlRunner sql_runner = new SqlRunner("SELECT COUNT(*) AS [count] FROM WideWorldImportersDW.Fact.Sale;", "(local)");
            Console.WriteLine(sql_runner.sql_data_reader.GetName(0));
            Console.WriteLine(sql_runner.sql_data_reader);
            Console.ReadKey();
        }
    }

}
