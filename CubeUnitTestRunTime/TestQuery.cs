using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeUnitTestRunTime
{
    interface TestQuery
    {
        string query_source_code { get; set; }
        string server_name { get; }
        List<string> column_names { get; }
        DataTable query_result_set { get; }
    }
}
