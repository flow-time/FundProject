using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FundNotice
{
    public static class SqlServerHelper
    {
        /*
         DbConnection can't open: https://learn.microsoft.com/en-us/answers/questions/69846/fixing-error-0-no-process-is-on-the-other-end-of-t
         */
        static IDbConnection dbConnection = new SqlConnection("Server=localhost;database=FundProject;User Id=sa;Password=Syndra!0428");
        public static int InsertDataToSqlServer(string sqlCommand, object body)
        {
            dbConnection.Open();
            int res = dbConnection.Execute(sqlCommand, body);
            dbConnection.Close();
            dbConnection.Dispose();
            return res;
        }

        public static T ExcuteSclacr<T>(string sqlCommand, object body)
        {
            dbConnection.Open();
            T res = dbConnection.ExecuteScalar<T>(sqlCommand, body);
            dbConnection.Close();
            dbConnection.Dispose();
            return res;
        }
    }
}
