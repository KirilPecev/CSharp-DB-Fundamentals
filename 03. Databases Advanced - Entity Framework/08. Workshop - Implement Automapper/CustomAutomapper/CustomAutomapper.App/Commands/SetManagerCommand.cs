namespace CustomAutomapper.App.Commands
{
    using Data;
    using Interfaces;
    using Models;

    public class SetManagerCommand : ICommand
    {
        private readonly CustomAutomapperContext _context;

        public SetManagerCommand(CustomAutomapperContext context)
        {
            this._context = context;
        }

        public string ExecuteCommand(string[] args)
        {
            int employeeId = int.Parse(args[0]);
            int managerId = int.Parse(args[1]);

            Employee employee = this._context.Employees.Find(employeeId);
            Employee manager = this._context.Employees.Find(managerId);

            employee.Manager = manager;

            this._context.SaveChanges();

            return "Manager was set successfully.";
        }
    }
}
