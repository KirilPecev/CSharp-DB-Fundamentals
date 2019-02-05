WITH RankedClients_CTE(Id, 
                       Names, 
                       Class, 
                       [Rank])
     AS (SELECT c.Id, 
                CONCAT(c.FirstName, ' ', c.LastName) AS Names, 
                m.Class, 
                DENSE_RANK() OVER(PARTITION BY CONCAT(c.FirstName, ' ', c.LastName)
                ORDER BY COUNT(m.Class) DESC) AS [Rank]
         FROM Clients AS c
              JOIN Orders AS o ON o.ClientId = c.Id
              JOIN Vehicles AS v ON v.Id = o.VehicleId
              JOIN Models AS m ON m.Id = v.ModelId
         GROUP BY c.id, 
                  CONCAT(c.FirstName, ' ', c.LastName), 
                  m.Class)

     SELECT k.Names, 
            k.Class
     FROM RankedClients_CTE AS k
     WHERE k.Rank = 1
     ORDER BY k.Names, 
              k.Class, 
              k.Id