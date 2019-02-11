SELECT c.Id, 
       concat(c.FirstName, ' ', c.LastName) AS full_name
FROM Colonists AS c
     JOIN TravelCards AS tc ON tc.ColonistId = c.Id
WHERE tc.JobDuringJourney = 'pilot'
ORDER BY c.Id