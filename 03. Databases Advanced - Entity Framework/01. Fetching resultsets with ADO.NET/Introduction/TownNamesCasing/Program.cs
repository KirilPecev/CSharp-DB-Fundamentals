namespace TownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    class Program
    {
        static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(
                "Server=.\\SQLEXPRESS01;" +
                "Database=MinionsDB;" +
                "Integrated Security=true"
            );

            string country = Console.ReadLine();

            string commandString = $@"SELECT COUNT(t.Id)
                                    FROM Towns AS t
                                    JOIN Countries AS c ON c.Id = t.CountryCode
                                    WHERE c.[Name] = @country";
            connection.Open();
            using (connection)
            {
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@country", country);
                    int count = (int)command.ExecuteScalar();

                    if (count > 0)
                    {
                        UpdateDb(connection, country);
                        Console.WriteLine($"{count} town names were affected.");
                        ReadFromDb(connection, country);
                    }
                    else
                    {
                        Console.WriteLine("No town names were affected.");
                    }
                }
            }
        }

        private static void ReadFromDb(SqlConnection connection, string countryName)
        {
            string commandString = $@"select Name from Towns
                                    WHERE CountryCode IN
                                   (
                                       SELECT Id
                                       FROM Countries
                                       WHERE [Name] = @countryName
                                   )
                                   ";

            List<string> towns = new List<string>();

            using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        towns.Add((string)reader["Name"]);
                    }
                }
            }

            Console.WriteLine($"[{string.Join(", ", towns)}]");
        }

        private static void UpdateDb(SqlConnection connection, string countryName)
        {
            string commandString = $@"UPDATE Towns
                   SET Name = UPPER(Name)
                 WHERE CountryCode = (SELECT c.Id FROM Countries AS c WHERE c.Name = @countryName)";

            using (SqlCommand command = new SqlCommand(commandString, connection))
            {
                command.Parameters.AddWithValue("@countryName", countryName);
                command.ExecuteNonQuery();
            }
        }
    }
}
