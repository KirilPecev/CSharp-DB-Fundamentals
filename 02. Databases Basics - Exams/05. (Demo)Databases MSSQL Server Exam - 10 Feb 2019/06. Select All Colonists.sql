SELECT Id, 
       CONCAT(FirstName, ' ', LastName) AS [FullName], 
       Ucn
FROM Colonists
ORDER BY FirstName, 
         LastName, 
         Id