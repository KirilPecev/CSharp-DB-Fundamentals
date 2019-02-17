SELECT TOP (2) CONCAT(m.FirstName, ' ', m.LastName) AS [Mechanic], 
               COUNT(j.JobId) AS [Jobs]
FROM Mechanics AS m
     JOIN Jobs AS j ON j.MechanicId = m.MechanicId
WHERE j.[Status] NOT IN('Finished')
GROUP BY m.FirstName, 
         m.LastName, 
         m.MechanicId
ORDER BY Jobs DESC, 
         m.MechanicId