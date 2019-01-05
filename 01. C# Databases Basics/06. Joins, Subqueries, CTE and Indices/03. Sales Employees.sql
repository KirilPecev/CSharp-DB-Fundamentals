SELECT e.EmployeeID, e.FirstName, e.LastName, d.Name AS [DepartmentName]
FROM Employees AS e,
Departments AS d
WHERE e.DepartmentID = d.DepartmentID AND d.Name = 'Sales'
ORDER BY e.EmployeeID
