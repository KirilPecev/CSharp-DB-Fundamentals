namespace CustomAutomapper.App.Commands
{
    using Data;
    using Interfaces;
    using System;
    using Automapper;
    using DTOs;
    using Microsoft.EntityFrameworkCore;
    using Models;

    public class SetBirthdayCommand : ICommand
    {
        private readonly CustomAutomapperContext _context;
        private readonly Mapper _mapper;

        public SetBirthdayCommand(CustomAutomapperContext context, Mapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public string ExecuteCommand(string[] args)
        {
            int employeeId = int.Parse(args[0]);
            DateTime birthday = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);

            Employee employee = this._context.Employees.Find(employeeId);

            EmployeeDto employeeDto = this._mapper.CreateMappedObject<EmployeeDto>(employee);
            employeeDto.Birthday = birthday;

            Employee updatedEmployee = this._mapper.CreateMappedObject<Employee>(employeeDto);

            this._context.Entry(employee).State = EntityState.Detached;
            this._context.Employees.Update(updatedEmployee);
            this._context.SaveChanges();

            return "Birthday was set successfully!";
        }
    }
}

