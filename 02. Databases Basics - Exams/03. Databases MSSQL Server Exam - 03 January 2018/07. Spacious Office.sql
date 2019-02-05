SELECT t.Name, 
       o.Name, 
       o.ParkingPlaces
FROM Offices AS o
     JOIN Towns AS t ON t.Id = o.TownId
WHERE o.ParkingPlaces > 25
ORDER BY t.Name, 
         o.Id