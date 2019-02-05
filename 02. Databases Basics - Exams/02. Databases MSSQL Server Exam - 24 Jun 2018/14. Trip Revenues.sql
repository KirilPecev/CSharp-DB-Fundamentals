SELECT act.TripId, 
       h.Name AS [HotelName], 
       r.Type AS [RoomType],
       CASE
           WHEN t.CancelDate IS NULL
           THEN SUM(h.BaseRate + r.Price)
           ELSE 0
       END AS [Revenue]
FROM Trips AS t
     JOIN Rooms AS r ON t.RoomId = r.Id
     JOIN Hotels AS h ON h.Id = r.HotelId
     JOIN AccountsTrips AS act ON act.TripId = t.Id
GROUP BY act.TripId, 
         h.Name, 
         r.Type, 
         t.CancelDate
ORDER BY RoomType, 
         act.TripId