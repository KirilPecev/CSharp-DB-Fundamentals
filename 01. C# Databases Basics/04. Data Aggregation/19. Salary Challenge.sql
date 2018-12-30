SELECT TOP (10) e.FirstName, e.LastName, e.DepartmentID
FROM Employees AS e
WHERE e.Salary > (SELECT AVG(e2.Salary)
				  FROM Employees as e2
				  WHERE e.DepartmentID = e2.DepartmentID)
ORDER BY DepartmentID
