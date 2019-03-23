namespace CustomAutomapper.App.Commands
{
    using Automapper;
    using Data;
    using DTOs;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Text;

    public class ManagerInfoCommand : ICommand
    {
        private readonly CustomAutomapperContext _context;
        private readonly Mapper _mapper;

        public ManagerInfoCommand(CustomAutomapperContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string ExecuteCommand(string[] args)
        {
            int managerId = int.Parse(args[0]);

            Employee manager = this._context.Employees
                .Include(x => x.Employees)
                .FirstOrDefault(e => e.Id == managerId);

            ManagerDto managerDto = this._mapper.CreateMappedObject<ManagerDto>(manager);

            StringBuilder builder = new StringBuilder();

            builder.AppendLine(
                $"{managerDto.FirstName} {managerDto.LastName} | Employees: {managerDto.Employees.Count}");

            foreach (var dto in managerDto.Employees)
            {
                builder.AppendLine($"     - {dto.FirstName} {dto.LastName} - ${dto.Salary:f2}");
            }

            return builder.ToString().TrimEnd();
        }
    }
}
