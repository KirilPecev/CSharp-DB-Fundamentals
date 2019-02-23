namespace MiniORM.App
{
    using MiniORM.App.Data;
    using MiniORM.App.Data.Entities;
    using System.Linq;

    public class StartUp
    {
        public static void Main(string[] args)
        {
            string connectionString = "Server =.\\SQLEXPRESS01; " +
                                      "Database=MiniORM;" +
                                      "Integrated Security=true";

            SoftUniDbContext context = new SoftUniDbContext(connectionString);

            Employee employee = new Employee()
            {
                FirstName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            };

            context.Employees.Add(employee);

            Employee currentEmployee = context.Employees.Last();
            employee.LastName = "Modified";

            context.SaveChanges();
        }
    }
}
