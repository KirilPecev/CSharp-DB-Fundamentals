SELECT TOP(5) e.EmployeeID, e.FirstName, e.Salary, d.Name AS [DepartmentName]
FROM Employees AS e,
Departments AS d
WHERE e.Salary>15000 AND e.DepartmentID = d.DepartmentID
ORDER BY d.DepartmentID