using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices;
using Microsoft.AnalysisServices.Core;
using Microsoft.AnalysisServices.Hosting;

namespace CubeUnitTestRunTime
{
    public class MdxRunner
    {
        public string cube_server_name { get; set; }
        public string cube_database_name { get; set; }
        public string cube_perspective_name { get; set; }
        public List<string> column_names { get; set; }

        public MdxRunner()
        {
            cube_server_name = "localhost";
            cube_database_name = "cube_database_name";

            string connection_string = $"Data source={cube_server_name}; initial catalog={cube_database_name}";
            //TODO populate column_names
        }
    }
}