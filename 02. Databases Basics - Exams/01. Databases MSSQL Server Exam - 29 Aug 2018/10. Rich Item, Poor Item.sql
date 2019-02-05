SELECT TOP(10) 
 		 o.Id,
		 MAX(i.Price) AS [ExpensivePrice],
		 MIN(i.Price) AS [CheapPrice]
    FROM OrderItems AS oi
    JOIN Orders AS o ON o.Id = oi.OrderId
    JOIN Items AS i ON i.Id = oi.ItemId
GROUP BY o.Id
ORDER BY ExpensivePrice DESC, o.Id