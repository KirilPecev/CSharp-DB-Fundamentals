  SELECT DATEPART(DAY, o.DateTime) AS [Day], 
  	     CAST(AVG(oi.Quantity*i.Price) AS decimal(18,2)) AS [Total profit]
    FROM Orders AS o
    JOIN OrderItems AS oi ON oi.OrderId = o.Id
    JOIN Items AS i ON i.Id = oi.ItemId
GROUP BY DATEPART(DAY, o.DateTime)
ORDER BY [Day]