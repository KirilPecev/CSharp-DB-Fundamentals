namespace InitialSetup
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    class Program
    {
        private const string connectionStringMinionsDB = "Server=.\\SQLEXPRESS01;" +
                                          "Database=MinionsDB;" +
                                          "Integrated Security=true";

        private const string connectionStringMaster = "Server=.\\SQLEXPRESS01;" +
                                                         "Database=master;" +
                                                         "Integrated Security=true";

        private static string query = File.ReadAllText(@"..\..\..\Minions DB Tables.sql");

        static void Main(string[] args)
        {
            string dbName = "MinionsDB";

            bool state = CreateDatabase(connectionStringMaster, dbName);

            if (!state)
            {
                return;
            }

            using (SqlConnection connection = new SqlConnection(connectionStringMinionsDB))
            {
                connection.Open();

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    try
                    {
                        command.ExecuteNonQuery();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Error: " + e.Message);
                        return;
                    }
                }
            }
            
            Console.WriteLine($"{dbName} created successfully!");
        }

        public static bool CreateDatabase(string dbConnectionString, string dbName)
        {
            using (SqlConnection connection = new SqlConnection(dbConnectionString))
            {
                connection.Open();
                string createDbCommand = $"CREATE DATABASE {dbName}";
                try
                {
                    using (SqlCommand command = new SqlCommand(createDbCommand, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error: " + e.Message);
                    return false;
                }
            }

            return true;
        }
    }
}
