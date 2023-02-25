using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace ElectronicJournal
{
    public class Database
    {
        private string connectionString;

        public Database()
        {
            connectionString = "Data Source=DESKTOP-3SM99MC;Initial Catalog=InstDB;Integrated Security=True";
        }

        public SqlConnection GetConnection()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }

}
