SELECT p.[Name] AS [PlanetName], 
       COUNT(j.Id) AS [JourneysCount]
FROM Planets AS p
     JOIN Spaceports AS sp ON sp.PlanetId = p.Id
     JOIN Journeys AS j ON j.DestinationSpaceportId = sp.Id
GROUP BY p.[Name]
ORDER BY JourneysCount DESC, 
         PlanetName