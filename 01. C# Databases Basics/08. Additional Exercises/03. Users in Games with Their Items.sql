SELECT u.Username AS [Username], 
       g.Name AS [Game], 
       COUNT(ugi.ItemId) AS [Items Count], 
       SUM(i.Price) AS [Items Price]
FROM Users AS u
     JOIN UsersGames AS ug ON ug.UserId = u.Id
     JOIN Games AS g ON g.Id = ug.GameId
     JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
     JOIN Items AS i ON i.Id = ugi.ItemId
GROUP BY u.Username, 
         g.Name
HAVING COUNT(ugi.ItemId) >= 10
ORDER BY [Items Count] DESC, 
         [Items Price] DESC, 
         Username