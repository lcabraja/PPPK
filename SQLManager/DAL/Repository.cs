using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLManager.DAL
{
    class Repository
    {
        private static string cs;
        private const string ConnectionString = "Server={0};Uid={1};Pwd={2}";
        internal static void LogIn(string server, string username, string password)
        {
            using (SqlConnection con = new SqlConnection(string.Format(ConnectionString, server, username, password)))
            {
                cs = con.ConnectionString;
                con.Open();
            }
        }
    }
}
