SELECT ISNULL(SUM(Price * Quantity), 0) as [Parts Total]
FROM Parts AS p
     JOIN OrderParts AS op ON op.PartId = p.PartId
     JOIN Orders AS o ON o.OrderId = op.OrderId
	 where DATEDIFF(WEEK, IssueDate, '20170424') <= 3