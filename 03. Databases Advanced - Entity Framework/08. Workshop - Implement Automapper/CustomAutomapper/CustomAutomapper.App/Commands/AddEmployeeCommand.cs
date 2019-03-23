namespace CustomAutomapper.App.Commands
{
    using Automapper;
    using Data;
    using DTOs;
    using Interfaces;
    using Models;

    public class AddEmployeeCommand : ICommand
    {
        private readonly CustomAutomapperContext _context;
        private readonly Mapper _mapper;

        public AddEmployeeCommand(CustomAutomapperContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string ExecuteCommand(string[] args)
        {
            string firstName = args[0];
            string lastName = args[1];
            decimal salary = decimal.Parse(args[2]);

            EmployeeDto employeeDto = new EmployeeDto()
            {
                FirstName = firstName,
                LastName = lastName,
                Salary = salary
            };

            Employee employee = _mapper.CreateMappedObject<Employee>(employeeDto);

            this._context.Employees.Add(employee);

            this._context.SaveChanges();

            return "Employee was registered successful!";
        }
    }
}
