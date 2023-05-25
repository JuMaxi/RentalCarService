using Microsoft.Extensions.Configuration;
using RentalCarService.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace RentalCarService.Services
{
    public class AccessDataBase : IAccessDataBase
    {
        string ConnectionString = " ";

        public AccessDataBase(IConfiguration Configuration ) 
        {
            ConnectionString = Configuration.GetSection("ConnectionString").Value;
        }

        public void AccessNonQuery (string Action)
        {
            using SqlConnection Connection = new SqlConnection(ConnectionString);
            {
                SqlCommand Command = new SqlCommand(Action, Connection);
                Connection.Open ();
                Command.ExecuteNonQuery ();
            }
        }

        public IDataReader AccessReader(string Action)
        {
            SqlConnection Connection = new SqlConnection(ConnectionString);
            SqlCommand Command = new SqlCommand(Action, Connection);
            SqlDataReader Reader = Command.ExecuteReader();
            return Reader;
        }
    }
}
