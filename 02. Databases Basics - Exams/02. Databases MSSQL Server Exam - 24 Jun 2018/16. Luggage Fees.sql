SELECT TripId, 
       SUM(Luggage) AS [Luggage],
       CASE
           WHEN SUM(Luggage) > 5
           THEN Concat('$', SUM(Luggage) * 5)
           ELSE Concat('$', 0)
       END AS [Fee]
FROM AccountsTrips
WHERE Luggage > 0
GROUP BY TripId
ORDER BY Luggage DESC