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
        SqlConnection connection;

        public Database()
        {
            connection = new SqlConnection("Data Source=DESKTOP-3SM99MC;Initial Catalog=InstDB;Integrated Security=True");
        }
        public void OpenConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
            {
                connection.Open();
            }
        }
        public void CloseConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
        }
        public SqlConnection GetConnection()
        {
            return connection;
        }
    }
}
