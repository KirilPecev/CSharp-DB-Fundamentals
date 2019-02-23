namespace AgeStoredProcedure
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

            connection.Open();

            int id = int.Parse(Console.ReadLine());

            string commandString = $@"EXEC usp_GetOlder @id";

            using (SqlCommand execProcedureCommand = new SqlCommand(commandString, connection))
            {
                execProcedureCommand.Parameters.AddWithValue("@id", id);
                execProcedureCommand.ExecuteNonQuery();

                commandString = $@"SELECT Name, Age FROM Minions WHERE Id = @id";
                using (SqlCommand command = new SqlCommand(commandString, connection))
                {
                    command.Parameters.AddWithValue("@id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Console.WriteLine($"{reader[0]} - {reader[1]} years old");
                        }
                    }
                }
            }

            connection.Close();
        }
    }
}
