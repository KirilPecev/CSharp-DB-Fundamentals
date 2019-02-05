SELECT k.[Category Name], 
       k.Email, 
       k.Bill, 
       k.Town
FROM
(
    SELECT c.Id, 
           CONCAT(c.FirstName, ' ', c.LastName) AS [Category Name], 
           c.Email, 
           o.Bill, 
           t.[Name] AS Town, 
           DENSE_RANK() OVER(PARTITION BY t.[Name]
           ORDER BY o.Bill DESC) AS [Rank]
    FROM Clients AS c
         JOIN Orders AS o ON o.ClientId = c.Id
         JOIN Towns AS t ON t.Id = o.TownId
    WHERE c.CardValidity < o.CollectionDate
) AS k
WHERE k.[Rank] IN(1, 2)
AND k.Bill IS NOT NULL
ORDER BY k.Town, 
         k.Bill, 
         k.Id