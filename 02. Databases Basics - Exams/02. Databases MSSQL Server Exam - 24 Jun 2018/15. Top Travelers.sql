SELECT AccountId, 
       Email, 
       CountryCode, 
       Trips
FROM
(
    SELECT A.Id AS AccountId, 
           A.Email, 
           C.CountryCode, 
           COUNT(*) AS Trips, 
           DENSE_RANK() OVER(PARTITION BY C.CountryCode
           ORDER BY COUNT(*) DESC, 
                    A.Id) AS [Rank]
    FROM Accounts A
         JOIN AccountsTrips AT ON A.Id = AT.AccountId
         JOIN Trips T ON AT.TripId = T.Id
         JOIN Rooms R ON T.RoomId = R.Id
         JOIN Hotels H ON R.HotelId = H.Id
         JOIN Cities C ON H.CityId = C.Id
    GROUP BY C.CountryCode, 
             A.Email, 
             A.Id
) AS RankedCountries
WHERE RankedCountries.Rank = 1
ORDER BY RankedCountries.Trips DESC, 
         RankedCountries.AccountId