namespace CustomAutomapper.App.Commands
{
    using Automapper;
    using Data;
    using DTOs;
    using Interfaces;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class ListEmployeesOlderThanCommand : ICommand
    {
        private readonly CustomAutomapperContext _context;
        private readonly Mapper _mapper;

        public ListEmployeesOlderThanCommand(CustomAutomapperContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string ExecuteCommand(string[] args)
        {
            int age = int.Parse(args[0]);

            Employee[] employees = this._context.Employees
                .Where(e => DateTime.Now.Year - e.Birthday.Value.Year > age)
                .OrderByDescending(e => e.Salary)
                .ToArray();

            Dictionary<EmployeeDto, string> maps = new Dictionary<EmployeeDto, string>();

            foreach (var e in employees)
            {
                EmployeeDto employeeDto = this._mapper.CreateMappedObject<EmployeeDto>(e);

                string manager;

                if (e.Manager == null)
                {
                    manager = "[no manager]";
                }
                else
                {
                    ManagerDto managerDto = this._mapper.CreateMappedObject<ManagerDto>(e.Manager);
                    manager = managerDto.LastName;
                }

                maps.Add(employeeDto, manager);
            }

            StringBuilder sb = new StringBuilder();

            foreach (var dto in maps)
            {
                sb.AppendLine(
                    $"{dto.Key.FirstName} {dto.Key.LastName} - ${dto.Key.Salary} - Manager: {dto.Value}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
