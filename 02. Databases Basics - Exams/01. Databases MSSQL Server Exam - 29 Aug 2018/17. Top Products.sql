  SELECT i.[Name] AS [Item],
  	     c.[Name] AS [Category],
		 SUM(oi.Quantity) AS [Count],
		 SUM(oi.Quantity * i.Price) AS [TotalPrice]
    FROM Orders AS o
    JOIN OrderItems AS oi ON oi.OrderId = o.Id
   RIGHT JOIN Items AS i ON i.Id = oi.ItemId
    JOIN Categories AS c ON c.Id = i.CategoryId
GROUP BY i.[Name], c.[Name]
ORDER BY TotalPrice DESC, [Count] DESC