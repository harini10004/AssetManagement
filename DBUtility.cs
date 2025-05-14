using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssestManagement
{
    public static class DBUtility
    {
        private const string connectionString = @"Data Source=HARINI;Initial Catalog=DigitalAssetDB;Integrated Security=True;MultipleActiveResultSets=true;";

        // Method to get and open SQL connection
        public static SqlConnection GetConnection()
        {
            SqlConnection connectionObject = new SqlConnection(connectionString);
            try
            {
                connectionObject.Open();
                return connectionObject;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error Opening the Connection: {e.Message}");
                return null;
            }
        }

        // Method to close and dispose connection
        public static void CloseDbConnection(SqlConnection connectionObject)
        {
            if (connectionObject != null)
            {
                try
                {
                    if (connectionObject.State == ConnectionState.Open)
                    {
                        connectionObject.Close();
                        connectionObject.Dispose();
                        Console.WriteLine("Connection closed");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Error Closing the Connection: {e.Message}");
                }
            }
            else
            {
                Console.WriteLine("Connection is already null");
            }
        }
    }
}
