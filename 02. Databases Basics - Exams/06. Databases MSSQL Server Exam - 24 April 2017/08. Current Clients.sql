SELECT concat(c.FirstName, ' ', c.LastName) AS Client, 
       DATEDIFF(DAY, j.IssueDate, '20170424') AS [Days going], 
       j.STATUS
FROM Clients AS c
     JOIN Jobs AS j ON j.ClientId = c.ClientId
WHERE j.STATUS NOT IN('Finished')
ORDER BY [Days going] DESC, 
         c.ClientId ASC