namespace CustomAutomapper.App.DTOs
{
    using System;

    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public decimal Salary { get; set; }

        public string Address { get; set; }

        public DateTime? Birthday { get; set; }
    }
}
