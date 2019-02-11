SELECT TOP (1) j.Id, 
               p.[Name] AS PlanetName, 
               sp.[Name] AS SpaceportName, 
               j.Purpose AS JourneyPurpose
FROM Journeys AS j
     JOIN Spaceports AS sp ON sp.Id = j.DestinationSpaceportId
     JOIN Planets AS p ON p.Id = sp.PlanetId
ORDER BY DATEDIFF(DAY, j.JourneyStart, j.JourneyEnd)