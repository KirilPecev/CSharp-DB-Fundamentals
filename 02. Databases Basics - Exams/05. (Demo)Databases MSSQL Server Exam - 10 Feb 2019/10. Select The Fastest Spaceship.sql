SELECT TOP (1) s.[Name] AS [SpaceshipName], 
               sp.[Name] AS [SpaceportName]
FROM Spaceships AS s
     JOIN Journeys AS j ON j.SpaceshipId = s.Id
     JOIN Spaceports AS sp ON sp.Id = j.DestinationSpaceportId
ORDER BY s.LightSpeedRate DESC