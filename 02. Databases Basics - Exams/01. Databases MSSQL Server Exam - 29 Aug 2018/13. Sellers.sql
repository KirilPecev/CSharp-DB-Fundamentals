SELECT TOP(10)
		e.FirstName + ' ' + e.LastName AS [Full Name],
		SUM(oi.Quantity*i.Price) AS [Total Price],
		SUM(oi.Quantity) AS [Items]
	FROM Employees AS e
	JOIN Orders AS o ON o.EmployeeId = e.Id
	JOIN OrderItems AS oi ON oi.OrderId = o.Id
	JOIN Items AS i ON i.Id = oi.ItemId
   WHERE o.DateTime < '2018-06-15'
GROUP BY e.FirstName, e.LastName
ORDER BY [Total Price] DESC, Items DESC
 