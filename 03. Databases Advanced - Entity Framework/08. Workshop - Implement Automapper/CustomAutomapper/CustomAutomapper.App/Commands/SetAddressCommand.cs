namespace CustomAutomapper.App.Commands
{
    using System.Linq;
    using Automapper;
    using Data;
    using DTOs;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SetAddressCommand : ICommand
    {
        private readonly CustomAutomapperContext _context;
        private readonly Mapper _mapper;

        public SetAddressCommand(CustomAutomapperContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string ExecuteCommand(string[] args)
        {
            int employeeId = int.Parse(args[0]);
            string address = string.Join(" ", args.Skip(1));

            Employee employee = this._context.Employees.Find(employeeId);

            EmployeeDto employeeDto = this._mapper.CreateMappedObject<EmployeeDto>(employee);
            employeeDto.Address = address;

            Employee updatedEmployee = this._mapper.CreateMappedObject<Employee>(employeeDto);

            this._context.Entry(employee).State = EntityState.Detached;
            this._context.Employees.Update(updatedEmployee);
            this._context.SaveChanges();

            return "Address was added successfully!";
        }
    }
}
