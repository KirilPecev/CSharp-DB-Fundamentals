namespace Add_Minion
{
    using System;
    using System.Data.SqlClient;
    using System.Linq;

    public class Program
    {
        public static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(
                "Server=.\\SQLEXPRESS01;" +
                "Database=MinionsDB;" +
                "Integrated Security=true"
            );

            string[] minionInfo = Console.ReadLine().Split().ToArray();
            string minionName = minionInfo[1];
            int age = int.Parse(minionInfo[2]);
            string town = minionInfo[3];

            string[] villainInfo = Console.ReadLine().Split().ToArray();
            string villainName = villainInfo[1];

            connection.Open();

            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                ProduceTownCommand(connection, transaction, town);

                ProduceVillainCommand(connection, transaction, villainName);

                ProduceInsertCommandForMinions(connection, transaction, minionName, age, villainName, town);

                transaction.Commit();

            }
            catch (Exception e)
            {
                Console.WriteLine("Error: " + e.Message);
                transaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
        }

        private static void ProduceInsertCommandForMinions(SqlConnection connection, SqlTransaction transaction, string minionName, int age, string villainName, string townName)
        {
            SqlCommand command = new SqlCommand($"SELECT Id FROM Towns WHERE Name = @townName", connection, transaction);
            command.Parameters.AddWithValue("@townName", townName);
            int townId = (int)command.ExecuteScalar();

            SqlCommand insertCommand = new SqlCommand($"INSERT INTO Minions VALUES(@minionName, @age, @townId)", connection, transaction);
            insertCommand.Parameters.AddWithValue("@minionName", minionName);
            insertCommand.Parameters.AddWithValue("@age", age);
            insertCommand.Parameters.AddWithValue("@townId", townId);
            insertCommand.ExecuteNonQuery();

            command = new SqlCommand($"select Id from Minions where Name = @minionName AND Age = @age", connection, transaction);
            command.Parameters.AddWithValue("@minionName", minionName);
            command.Parameters.AddWithValue("@age", age);
            int minionId = (int)command.ExecuteScalar();

            command = new SqlCommand($"select Id from Villains where Name = @villainName", connection, transaction);
            command.Parameters.AddWithValue("@villainName", villainName);
            int villainId = (int)command.ExecuteScalar();

            insertCommand = new SqlCommand($"INSERT INTO MinionsVillains VALUES(@minionId, @villainId)", connection, transaction);
            insertCommand.Parameters.AddWithValue("@minionId", minionId);
            insertCommand.Parameters.AddWithValue("@villainId", villainId);
            insertCommand.ExecuteNonQuery();

            Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
        }

        private static void ProduceVillainCommand(SqlConnection connection, SqlTransaction transaction, string villainName)
        {
            string commandString = $@"SELECT [Name] FROM Villains WHERE [Name] = @villainName";

            SqlCommand command = new SqlCommand(commandString, connection, transaction);
            command.Parameters.AddWithValue("@villainName", villainName);

            string name = (string)command.ExecuteScalar();

            if (name == null)
            {
                ProduceInsertCommand(connection, transaction, "Villains", villainName);
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }
        }

        private static void ProduceTownCommand(SqlConnection connection, SqlTransaction transaction, string townName)
        {
            string townCommand = $@"SELECT Name FROM Towns WHERE Name = @townName";

            SqlCommand command = new SqlCommand(townCommand, connection, transaction);
            command.Parameters.AddWithValue("@townName", townName);

            string name = (string)command.ExecuteScalar();

            if (name == null)
            {
                ProduceInsertCommand(connection, transaction, "Towns", townName);
                Console.WriteLine($"Town {townName} was added to the database.");
            }
        }

        private static void ProduceInsertCommand(SqlConnection connection, SqlTransaction transaction, string table, string value)
        {
            SqlCommand command;

            if (table == "Villains")
            {
                command = new SqlCommand($"INSERT INTO {table}(Name, EvilnessFactorId) VALUES(@value, 4)", connection, transaction);
                command.Parameters.AddWithValue("@value", value);
            }
            else
            {
                command = new SqlCommand($"INSERT INTO {table}(Name) VALUES(@value)", connection, transaction);
                command.Parameters.AddWithValue("@value", value);
            }

            command.ExecuteNonQuery();
        }
    }
}
