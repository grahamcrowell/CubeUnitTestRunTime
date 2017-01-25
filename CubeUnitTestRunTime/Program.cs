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
        
        static void foo()
        {
            string sql = @"
SELECT 
	AVG(ref_wt.WaitTimeDays) AS [Average Wait Time]
	,wt_dim.WaitTimeMeasureDesc AS [Wait Time Measure]
	,end_dt.FiscalPeriodLong AS [Fiscal Year Period]
	,end_age.Age AS [Age at End Date]
FROM CommunityMart.dbo.ReferralWaitTimeFact AS ref_wt
JOIN CommunityMart.Dim.WaitTimeMeasure AS wt_dim
ON ref_wt.WaitTimeMeasureID = wt_dim.WaitTimeMeasureID
JOIN CommunityMart.Dim.Date AS st_dt
ON ref_wt.StartDateID = st_dt.DateID
JOIN CommunityMart.Dim.Date AS end_dt
ON ref_wt.EndDateID = end_dt.DateID
JOIN CommunityMart.Dim.Age AS st_age
ON ref_wt.StartAgeID = st_age.AgeID
JOIN CommunityMart.Dim.Age AS end_age
ON ref_wt.EndAgeID = end_age.AgeID


JOIN CommunityMart.Dim.LocalReportingOffice AS lro
ON lro.LocalReportingOfficeID = ref_wt.LocalReportingOfficeID

GROUP BY wt_dim.WaitTimeMeasureDesc
	,end_dt.FiscalPeriodLong
	,end_age.Age
ORDER BY wt_dim.WaitTimeMeasureDesc
	,end_dt.FiscalPeriodLong
	,end_age.Age
";
            SqlRunner sql_runner = new SqlRunner(sql, "STDBDECSUP01");

            foreach (string column_name in sql_runner.column_names)
            {
                Console.WriteLine(column_name);
            }



            MdxRunner.make_mdx_runner(sql_runner, "STDSDB004\tabular", "ReferralWaitTimeCube");



//            string mdx = @"
//SELECT 
//	{[Measures].[Unique Referrals]} ON COLUMNS
//	,{[Fiscal Date].[Fiscal Year].ALLMEMBERS * [Referral Wait Time].[Paris Team].ALLMEMBERS} ON ROWS
//FROM [Model]
//";
//            MdxRunner mdx_runner = new MdxRunner(mdx, "STDSDB004\tabular", "ReferralWaitTimeCube");
//            foreach (string column_name in mdx_runner.column_names)
//            {
//                Console.WriteLine(column_name);
//            }



        }
        static void Main(string[] args)
        {
            foo();
            Console.ReadKey();
        }
    }

}
