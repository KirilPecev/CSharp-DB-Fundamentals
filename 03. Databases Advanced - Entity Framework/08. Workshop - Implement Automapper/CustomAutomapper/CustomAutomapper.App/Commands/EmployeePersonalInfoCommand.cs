namespace CustomAutomapper.App.Commands
{
    using Automapper;
    using Data;
    using DTOs;
    using Interfaces;
    using Models;

    public class EmployeePersonalInfoCommand : ICommand
    {
        private readonly CustomAutomapperContext _context;
        private readonly Mapper _mapper;

        public EmployeePersonalInfoCommand(CustomAutomapperContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string ExecuteCommand(string[] args)
        {
            int employeeId = int.Parse(args[0]);

            Employee employee = this._context.Employees.Find(employeeId);

            EmployeeDto employeeDto = this._mapper.CreateMappedObject<EmployeeDto>(employee);

            string birthday = employeeDto.Birthday.Value.Date.ToString("dd-MM-yyyy") ?? "information not found";
            string address = employeeDto.Address ?? "information not found";

            string result = 
                $"ID: {employeeDto.Id} - {employeeDto.FirstName} {employeeDto.LastName} - ${employeeDto.Salary:f2}\n"
                + $"Birthday: {birthday}\n"
                + $"Address: {address}";

            return result;
        }
    }
}
