SELECT top (1) k.Name, 
       k.[Times Serviced], 
       ISNULL(SUM(p.Price * op.Quantity), 0) AS [Parts Total]
FROM
(
    SELECT m.ModelId, 
           m.[Name], 
           COUNT(j.JobId) AS [Times Serviced], 
           DENSE_RANK() OVER(
           ORDER BY COUNT(j.JobId) DESC) AS [Rank]
    FROM Models AS m
         JOIN Jobs AS j ON j.ModelId = m.ModelId
    GROUP BY m.ModelId, 
             m.[Name]
) AS k
JOIN Jobs AS j ON j.ModelId = k.ModelId
JOIN Orders AS o ON o.JobId = j.JobId
JOIN OrderParts AS op ON op.OrderId = o.OrderId
JOIN Parts AS p ON p.PartId = op.PartId
WHERE k.Rank = 1
GROUP BY k.Name, 
         k.[Times Serviced]
ORDER BY [Parts Total]

----------------------------------------------------------------

SELECT TOP (1) m.Name AS Model, 
               COUNT(*) AS [Times Serviced], 
(
    SELECT ISNULL(SUM(p.Price * op.Quantity), 0) AS [Parts Total]
    FROM Parts AS p
         JOIN OrderParts AS op ON op.PartId = p.PartId
         JOIN Orders AS o ON o.OrderId = op.OrderId
         JOIN Jobs AS j ON j.JobId = o.JobId
    WHERE j.ModelId = m.ModelId
) AS [Parts Total]
FROM Models AS m
     JOIN Jobs AS j ON j.ModelId = m.ModelId
GROUP BY m.ModelId, 
         m.Name
ORDER BY [Times Serviced] DESC;