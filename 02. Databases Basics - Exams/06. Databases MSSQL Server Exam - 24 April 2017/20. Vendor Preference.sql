WITH cte_PartsQuantity
     AS (SELECT m.MechanicId, 
                v.VendorId, 
                SUM(op.Quantity) AS [PartsCount]
         FROM Mechanics AS m
              JOIN Jobs AS j ON j.MechanicId = m.MechanicId
              JOIN Orders AS o ON o.JobId = j.JobId
              JOIN OrderParts AS op ON op.OrderId = o.OrderId
              JOIN Parts AS p ON p.PartId = op.PartId
              JOIN Vendors AS v ON v.VendorId = p.VendorId
         GROUP BY m.MechanicId, 
                  v.VendorId)

     SELECT CONCAT(m.FirstName, ' ', m.LastName) AS [Mechanic], 
            v.[Name] AS [Vendor], 
            cte.PartsCount AS [Parts], 
            CONCAT(FLOOR(cte.PartsCount * 1.0 /
     (
         SELECT SUM(PartsCount)
         FROM cte_PartsQuantity
         WHERE MechanicId = m.MechanicId
     ) * 100), '%') AS [Preference]
     FROM Mechanics AS m
          JOIN cte_PartsQuantity AS cte ON cte.MechanicId = m.MechanicId
          JOIN Vendors AS v ON v.VendorId = cte.VendorId
     ORDER BY Mechanic, 
              Parts DESC, 
              Vendor

