SELECT DISTINCT e.Id,
	   e.FirstName AS [First Name],
	   e.LastName AS [Last Name]
  FROM Employees AS e
  JOIN Orders AS o ON o.EmployeeId = e.Id
ORDER BY e.Id