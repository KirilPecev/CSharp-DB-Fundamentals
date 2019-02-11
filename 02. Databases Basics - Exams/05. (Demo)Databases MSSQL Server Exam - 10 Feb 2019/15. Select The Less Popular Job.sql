SELECT TOP (1) tc.JourneyId, 
               tc.JobDuringJourney
FROM Colonists AS c
     JOIN TravelCards AS tc ON tc.ColonistId = c.Id
WHERE tc.JourneyId =
(
    SELECT TOP (1) Id
    FROM Journeys
    ORDER BY DATEDIFF(DAY, JourneyStart, JourneyEnd) DESC
)
GROUP BY tc.JourneyId, 
         tc.JobDuringJourney
ORDER BY COUNT(c.Id) ASC