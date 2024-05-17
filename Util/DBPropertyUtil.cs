using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HosManagement.Util
{
    public static class DBPropertyUtil
    {
        public static class DBConnection
        {

            private static SqlConnection connection;

            public static SqlConnection GetConnection()
            {
                // Get the connection string
                string connectionString = PropertyUtil.GetPropertyString();

                // Create a SqlConnection object
                SqlConnection connection = new SqlConnection(connectionString);

                return connection;
            }

        }
    }
    }
