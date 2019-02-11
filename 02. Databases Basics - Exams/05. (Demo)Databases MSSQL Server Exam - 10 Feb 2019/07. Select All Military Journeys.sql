SELECT id, 
       Format(JourneyStart, 'dd/MM/yyyy') AS JourneyStart, 
       Format(JourneyEnd, 'dd/MM/yyyy') AS JourneyEnd
FROM Journeys
WHERE Purpose = 'Military'
ORDER BY JourneyStart