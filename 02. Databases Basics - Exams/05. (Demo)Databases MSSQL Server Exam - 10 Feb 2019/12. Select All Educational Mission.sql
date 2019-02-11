SELECT p.[Name], 
       sp.[Name]
FROM Planets AS p
     JOIN Spaceports AS sp ON sp.PlanetId = p.id
     JOIN Journeys AS j ON j.DestinationSpaceportId = sp.Id
                           AND j.Purpose = 'educational'
ORDER BY sp.[Name] DESC