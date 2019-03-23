namespace CustomAutomapper.App.Commands
{
    using Automapper;
    using Data;
    using DTOs;
    using Interfaces;
    using Models;

    public class EmployeeInfoCommand : ICommand
    {
        private readonly CustomAutomapperContext _context;
        private readonly Mapper _mapper;

        public EmployeeInfoCommand(CustomAutomapperContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string ExecuteCommand(string[] args)
        {
            int employeeId = int.Parse(args[0]);

            Employee employee = this._context.Employees.Find(employeeId);

            EmployeeDto employeeDto = this._mapper.CreateMappedObject<EmployeeDto>(employee);

            string result = $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}";

            return result;
        }
    }
}
