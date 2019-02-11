SELECT *
FROM
(
    SELECT tc.JobDuringJourney, 
           CONCAT(c.FirstName, ' ', c.LastName) AS [FullName], 
           DENSE_RANK() OVER(PARTITION BY tc.JobDuringJourney
           ORDER BY c.BirthDate ASC) AS [JobRank]
    FROM Colonists AS c
         JOIN TravelCards AS tc ON tc.ColonistId = c.Id
         JOIN Journeys AS j ON j.Id = tc.JourneyId
) AS k
WHERE k.JobRank = 2