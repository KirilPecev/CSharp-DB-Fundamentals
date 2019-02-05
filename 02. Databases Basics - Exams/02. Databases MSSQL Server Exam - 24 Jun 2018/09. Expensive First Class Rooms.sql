SELECT r.Id, 
       r.Price, 
       h.Name AS [Hotel], 
       c.Name AS [City]
FROM Rooms AS r
     JOIN Hotels AS h ON h.Id = r.HotelId
     JOIN Cities AS c ON c.Id = h.CityId
WHERE Type = 'First Class'
ORDER BY Price DESC, 
         Id