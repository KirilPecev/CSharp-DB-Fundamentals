SELECT CONCAT(m.FirstName, ' ', m.LastName) AS [Available]
FROM Mechanics AS m
WHERE m.MechanicId NOT IN
(
    SELECT DISTINCT 
           m.MechanicId
    FROM Mechanics AS m
         JOIN Jobs AS j ON j.MechanicId = m.MechanicId
    WHERE j.[Status] = 'In Progress'
)
ORDER BY m.MechanicId