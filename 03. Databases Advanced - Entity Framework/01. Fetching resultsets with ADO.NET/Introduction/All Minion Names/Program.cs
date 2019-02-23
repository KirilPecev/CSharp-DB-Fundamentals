namespace All_Minion_Names
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

            List<string> names = new List<string>();

            connection.Open();
            using (connection)
            {
                string commandStrng = $@"SELECT Name FROM Minions";

                using (SqlCommand command = new SqlCommand(commandStrng, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        while (reader.Read())
                        {
                            names.Add((string)reader[0]);
                        }
                    }
                }
            }

            int count = 0;

            for (int i = 0; i < names.Count; i++)
            {
                if (count >= names.Count)
                {
                    break;
                }

                Console.WriteLine(names[i]);
                Console.WriteLine(names[names.Count - 1 - i]);
                count += 2;
            }
        }
    }
}
