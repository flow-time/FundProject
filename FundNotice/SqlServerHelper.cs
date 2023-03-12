using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace FundNotice
{
    public class SqlServerHelper
    {
        IDbConnection dbConnection;
        public void sasd() {

            dbConnection.ConnectionString = "";
            dbConnection.Open();
            dbConnection.Execute("", new { a=1});
        }
    }
}
