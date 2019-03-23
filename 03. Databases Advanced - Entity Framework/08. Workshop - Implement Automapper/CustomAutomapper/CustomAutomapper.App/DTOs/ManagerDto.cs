namespace CustomAutomapper.App.DTOs
{
    using System.Collections.Generic;

    public class ManagerDto
    {
        public ManagerDto()
        {
            this.Employees = new List<EmployeeDto>();
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<EmployeeDto> Employees { get; set; }
    }
}
