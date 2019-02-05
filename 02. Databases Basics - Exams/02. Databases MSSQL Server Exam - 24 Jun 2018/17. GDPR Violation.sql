SELECT t.Id, 
       a.FirstName + ' ' + ISNULL(MiddleName + ' ', '') + a.LastName AS [Full Name], 
       c.Name AS [From], 
       ch.Name AS [To],
       CASE
           WHEN t.CancelDate IS NOT NULL
           THEN 'Canceled'
           ELSE CAST(DATEDIFF(DAY, ArrivalDate, ReturnDate) AS NVARCHAR) + ' days'
       END AS [Duration]
FROM Trips AS t
     JOIN AccountsTrips AS act ON act.TripId = t.Id
     JOIN Accounts AS a ON a.Id = act.AccountId
     JOIN Cities AS c ON c.Id = a.CityId
     JOIN Rooms AS r ON r.Id = t.RoomId
     JOIN Hotels AS h ON h.Id = r.HotelId
	 join Cities as ch on ch.Id = h.CityId
ORDER BY [Full Name], 
         t.Id