namespace CustomAutomapper.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class CustomAutomapperContext : DbContext
    {
        public CustomAutomapperContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Employee> Employees { get; set; }
    }
}
