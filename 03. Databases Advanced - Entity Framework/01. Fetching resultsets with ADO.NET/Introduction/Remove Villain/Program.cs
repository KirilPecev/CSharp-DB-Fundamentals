namespace Remove_Villain
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

            int id = int.Parse(Console.ReadLine());

            connection.Open();
            using (connection)
            {
                string villainName = GetVillainName(connection, id);

                int count = GetMinionsCount(connection, id);

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{count} minions were released.");

                ProduceDeleteCommand(connection, id);
            }
        }

        private static void ProduceDeleteCommand(SqlConnection connection, int id)
        {
            string deleteCommandString = $@"DELETE FROM MinionsVillains
                                    WHERE VillainId = @id
                                    
                                    DELETE FROM Villains
                                    WHERE Id = @id";

            using (SqlCommand command = new SqlCommand(deleteCommandString, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }

        private static int GetMinionsCount(SqlConnection connection, int id)
        {
            string releasedMinionsCommandString = $@"SELECT COUNT(*) FROM MinionsVillains WHERE VillainId = @id";
            int count;

            using (SqlCommand command = new SqlCommand(releasedMinionsCommandString, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                count = (int)command.ExecuteScalar();
            }

            return count;
        }

        private static string GetVillainName(SqlConnection connection, int id)
        {
            string villainNameCommandString = $@"SELECT Name FROM Villains WHERE Id = @id";
            string name = "";

            using (SqlCommand command = new SqlCommand(villainNameCommandString, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                name = (string)command.ExecuteScalar();

                if (name == null)
                {
                    Console.WriteLine("No such villain was found.");
                    connection.Close();
                    Environment.Exit(0);
                }
            }

            return name;
        }
    }
}
