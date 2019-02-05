SELECT e.FirstName,
	   e.LastName,
	   AVG(DATEDIFF(HOUR, s.CheckIn, s.CheckOut)) AS [Work hours]
  FROM Employees AS e
  JOIN Shifts AS s ON s.EmployeeId = e.Id
GROUP BY e.Id, e.FirstName, e.LastName
HAVING AVG(DATEDIFF(HOUR, s.CheckIn, s.CheckOut)) > 7
ORDER BY [Work hours] DESC, e.Id