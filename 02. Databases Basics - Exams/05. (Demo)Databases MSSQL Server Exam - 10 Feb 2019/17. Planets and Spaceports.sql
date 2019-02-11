SELECT p.[Name], 
       COUNT(sp.Id) AS [Count]
FROM Planets AS p
     LEFT JOIN Spaceports AS sp ON sp.PlanetId = p.Id
GROUP BY p.[Name]
ORDER BY [Count] DESC, 
         p.[Name]