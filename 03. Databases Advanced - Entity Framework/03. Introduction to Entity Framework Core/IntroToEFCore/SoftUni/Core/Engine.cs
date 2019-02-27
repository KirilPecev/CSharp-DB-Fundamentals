namespace SoftUni.Core
{
    using Microsoft.EntityFrameworkCore;
    using SoftUni.Data;
    using SoftUni.Models;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Text;

    public class Engine
    {
        public void Run()
        {         
            using (SoftUniContext context = new SoftUniContext())
            {
                string result = GetEmployeesFullInformation(context);
                Console.WriteLine(result);
            }
        }

        //Problem 03. Employees Full Information
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Employee[] employees = context.Employees
                .OrderBy(e => e.EmployeeId)
                .ToArray();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:f2}");
            }

            return sb.ToString().Trim();
        }

        //Problem 04. Employees with Salary Over 50 000
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new { e.FirstName, e.Salary })
                .OrderBy(e => e.FirstName)
                .ToArray();

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} - {e.Salary:f2}");
            }

            return sb.ToString().Trim();
        }

        //Problem 05. Employees from Research and Development
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new { e.FirstName, e.LastName, DepartmentName = e.Department.Name, e.Salary })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName);

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:f2}");
            }

            return sb.ToString().Trim();
        }

        //Problem 06. Adding a New Address and Updating Employee
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Employee employee = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            Address adress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            employee.Address = adress;
            context.SaveChanges();

            var employees = context.Employees
                .Select(e => new { e.AddressId, e.Address.AddressText })
                .OrderByDescending(e => e.AddressId)
                .Take(10);

            foreach (var e in employees)
            {
                sb.AppendLine($"{string.Join(Environment.NewLine, e.AddressText)}");
            }

            return sb.ToString().Trim();
        }

        //Problem 07. Employees and Projects
        public static string GetEmployeesInPeriod(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.EmployeesProjects.Any(p =>
                    p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    ManagerFirstName = e.Manager.FirstName,
                    ManagerLastName = e.Manager.LastName,
                    Projects = e.EmployeesProjects
                                        .Select(ep => ep.Project)
                })
                .Take(10);

            foreach (var e in employees)
            {
                sb.AppendLine($"{e.FirstName} {e.LastName} - Manager: {e.ManagerFirstName} {e.ManagerLastName}");
                foreach (var project in e.Projects)
                {
                    sb.AppendLine(
                        $"--{project.Name} - {project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.GetCultureInfo("en-US"))} - {(project.EndDate == null ? "not finished" : project.EndDate?.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.GetCultureInfo("en-US")))}");
                }
            }

            return sb.ToString().Trim();
        }

        //Problem 08. Addresses by Town
        public static string GetAddressesByTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var addresses = context.Addresses
                .Select(a => new { a.AddressText, Town = a.Town.Name, EmployeeCount = a.Employees.Count })
                .OrderByDescending(a => a.EmployeeCount)
                .ThenBy(a => a.Town)
                .ThenBy(a => a.AddressText)
                .Take(10);

            foreach (var a in addresses)
            {
                sb.AppendLine($"{a.AddressText}, {a.Town} - {a.EmployeeCount} employees");
            }

            return sb.ToString().Trim();
        }

        //Problem 09. Employee 147
        public static string GetEmployee147(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Employee employee = context.Employees.Find(147);

            var projects = context.EmployeesProjects
                .Where(e => e.EmployeeId == employee.EmployeeId)
                .Select(p => p.Project);

            sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");
            foreach (var p in projects.OrderBy(p => p.Name))
            {
                sb.AppendLine(p.Name);
            }

            return sb.ToString().Trim();
        }

        //Problem 10. Departments with More Than 5 Employees
        public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var departments = context.Departments
                .Where(d => d.Employees.Count > 5)
                .Select(d => new
                {
                    d.Name,
                    ManagerFirstName = d.Manager.FirstName,
                    ManagerLastName = d.Manager.LastName,
                    EmployeeCount = d.Employees.Count,
                    d.Employees
                })
                .OrderBy(d => d.EmployeeCount)
                .ThenBy(d => d.Name)
                .ToArray();

            foreach (var d in departments)
            {
                sb.AppendLine($"{d.Name} - {d.ManagerFirstName} {d.ManagerLastName}");
                foreach (var e in d.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    sb.AppendLine($"{e.FirstName} {e.LastName} - {e.JobTitle}");
                }
            }

            return sb.ToString().Trim();
        }

        //Problem 11. Find Latest 10 Projects
        public static string GetLatestProjects(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var projects = context.Projects
                .OrderByDescending(p => p.StartDate)
                .Take(10);

            foreach (var p in projects.OrderBy(p => p.Name))
            {
                sb.AppendLine($"{p.Name}");
                sb.AppendLine($"{p.Description}");
                sb.AppendLine($"{p.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.GetCultureInfo("en-US"))}");
            }

            return sb.ToString().Trim();
        }

        //Problem 12. Increase Salaries
        public static string IncreaseSalaries(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            string[] departments = new[]
            {
                "Engineering", "Tool Design", "Marketing", "Information Services"
            };

            var employees = context.Employees
                .Include(e => e.Department)
                .Where(e => departments.Contains(e.Department.Name))
                .ToArray();

            foreach (var e in employees)
            {
                e.Salary += e.Salary * 0.12m;
            }

            context.SaveChanges();

            foreach (var employee in employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} (${employee.Salary:F2})");
            }

            return sb.ToString().Trim();
        }

        //Problem 13. Find Employees by First Name Starting With "Sa"
        public static string GetEmployeesByFirstNameStartingWithSa(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            var employees = context.Employees
                .Where(e => e.FirstName.StartsWith("Sa"))
                .OrderBy(e => e.FirstName)
                .ThenBy(e => e.LastName);


            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle} - (${employee.Salary:F2})");
            }

            return sb.ToString().Trim();
        }

        //Problem 14. Delete Project by Id
        public static string DeleteProjectById(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Project project = context.Projects
                .FirstOrDefault(p => p.ProjectId == 2);

            EmployeeProject[] employeesProjects = context.EmployeesProjects
                .Where(ep => ep.ProjectId == 2)
                .ToArray();

            context.EmployeesProjects.RemoveRange(employeesProjects);

            context.Projects.Remove(project);

            context.SaveChanges();

            var projects = context.Projects.Take(10);

            foreach (var p in projects)
            {
                sb.AppendLine($"{p.Name}");
            }

            return sb.ToString().Trim();
        }

        //Problem 15. Remove Town
        public static string RemoveTown(SoftUniContext context)
        {
            StringBuilder sb = new StringBuilder();

            Town town = context.Towns.FirstOrDefault(t => t.Name == "Seattle");

            Address[] addresses = context.Addresses
                .Where(a => a.Town.Name == "Seattle")
                .ToArray();

            int count = addresses.Length;

            var employees = context.Employees
                .Where(e => addresses.Contains(e.Address));

            foreach (var e in employees)
            {
                e.Address = null;
            }

            context.Addresses.RemoveRange(addresses);

            context.Towns.Remove(town);

            context.SaveChanges();

            var projects = context.Projects.Take(10);

            return $"{count} addresses in Seattle were deleted";
        }
    }
}
