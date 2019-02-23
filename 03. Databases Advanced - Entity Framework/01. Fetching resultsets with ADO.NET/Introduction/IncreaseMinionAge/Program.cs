namespace IncreaseMinionAge
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(
                "Server=.\\SQLEXPRESS01;" +
                "Database=MinionsDB;" +
                "Integrated Security=true"
            );

            int[] ids = Console.ReadLine().Split().Select(int.Parse).ToArray();

            connection.Open();

            using (connection)
            {
                IncrementAgeOfMinions(connection, ids);
                PrintNames(connection);
            }
        }

        private static void PrintNames(SqlConnection connection)
        {
            string commandString = $@"SELECT Name, Age FROM Minions";

            using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine($"{(string)reader[0]} {(int)reader[1]}");
                    }
                }
            }
        }

        private static void IncrementAgeOfMinions(SqlConnection connection, int[] ids)
        {
            using (SqlCommand command = new SqlCommand())
            {
                foreach (var minionId in ids)
                {
                    string commandString = $@"UPDATE Minions SET Age +=1 WHERE Id = {minionId}";
                    command.CommandText = commandString;
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
