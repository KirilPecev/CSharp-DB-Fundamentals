  SELECT e.FirstName + ' ' + e.LastName AS [Full Name],
         DATENAME(WEEKDAY,s.CheckIn) AS [DayOfWeek]
    FROM Employees AS e
	JOIN Shifts AS s ON s.EmployeeId = e.Id
	LEFT JOIN Orders AS o ON o.EmployeeId = e.Id
   WHERE DATEDIFF(HOUR,s.CheckIn,s.CheckOut) > 12 AND o.Id IS NULL
ORDER BY e.Id
