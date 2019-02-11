SELECT s.[Name], 
       s.Manufacturer
FROM Spaceships AS s
     JOIN Journeys AS j ON j.SpaceshipId = s.Id
     JOIN TravelCards AS tc ON tc.JourneyId = j.Id
     JOIN Colonists AS c ON c.Id = tc.ColonistId
WHERE DATEDIFF(YEAR, c.BirthDate, '01/01/2019') < 30
      AND tc.JobDuringJourney = 'pilot'
ORDER BY s.[Name]