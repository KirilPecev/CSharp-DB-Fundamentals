namespace Minion_Names
{
    using System;
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

            int villainId = int.Parse(Console.ReadLine());

            string commandString = $@"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                         m.Name as MinionName, 
                                         m.Age
                                    FROM MinionsVillains AS mv
                                    JOIN Minions As m ON mv.MinionId = m.Id
                                   WHERE mv.VillainId = @Id
                                ORDER BY m.Name";

            connection.Open();

            using (connection)
            {
                using (SqlCommand command = new SqlCommand($"SELECT Name FROM Villains WHERE Id = @Id", connection))
                {
                    command.Parameters.AddWithValue("@Id", villainId);

                    string villainName = (string)command.ExecuteScalar();

                    if (villainName == null)
                    {
                        Console.WriteLine($"No villain with ID {villainId} exists in the database.");
                        return;
                    }

                    using (SqlCommand secondCommand = new SqlCommand(commandString, connection))
                    {
                        secondCommand.Parameters.AddWithValue("@Id", villainId);

                        SqlDataReader reader = secondCommand.ExecuteReader();
                        using (reader)
                        {
                            Console.WriteLine($"Villain: {villainName}");

                            if (!reader.HasRows)
                            {
                                Console.WriteLine("(no minions)");
                                return;
                            }

                            while (reader.Read())
                            {
                                string minionName = reader["MinionName"].ToString();
                                int age = (int)reader["Age"];

                                Console.WriteLine($"{reader["RowNum"]}. {minionName} {age}");
                            }
                        }
                    }
                }
            }
        }
    }
}
