SELECT TOP(50)
	e.EmployeeID, 
	e.FirstName + ' ' + e.LastName AS [EmployeeName], 
	mng.FirstName + ' ' + mng.LastName AS [ManagerName],
	dept.Name AS [DepartmentName]
FROM Employees AS e
	JOIN Employees AS mng
	ON mng.EmployeeID = e.ManagerID
	JOIN Departments as dept
	ON dept.DepartmentID = e.DepartmentID
ORDER BY e.EmployeeID