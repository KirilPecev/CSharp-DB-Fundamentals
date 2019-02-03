SELECT u.Username AS [Username], 
       g.Name AS [Game], 
       MAX(c.Name) AS [Character], 
       SUM(si.Strength) + MAX(sgt.Strength) + MAX(sc.Strength) AS [Strength], 
       SUM(si.Defence) + MAX(sgt.Defence) + MAX(sc.Defence) AS [Defence], 
       SUM(si.Speed) + MAX(sgt.Speed) + MAX(sc.Speed) AS [Speed], 
       SUM(si.Mind) + MAX(sgt.Mind) + MAX(sc.Mind) AS [Mind], 
       SUM(si.Luck) + MAX(sgt.Luck) + MAX(sc.Luck) AS [Luck]
FROM Users AS u
     JOIN UsersGames AS ug ON ug.UserId = u.Id
     JOIN Games AS g ON g.Id = ug.GameId
     JOIN GameTypes AS gt ON gt.Id = g.GameTypeId
     JOIN [Statistics] AS sgt ON sgt.id = gt.BonusStatsId
     JOIN Characters AS c ON c.Id = ug.CharacterId
     JOIN [Statistics] AS sc ON sc.id = c.StatisticId
     JOIN UserGameItems AS ugi ON ugi.UserGameId = ug.Id
     JOIN Items AS i ON i.Id = ugi.ItemId
     JOIN [Statistics] AS si ON si.id = i.StatisticId
GROUP BY u.Username, 
         g.Name
ORDER BY Strength DESC, 
         Defence DESC, 
         Speed DESC, 
         mind DESC, 
         Luck DESC