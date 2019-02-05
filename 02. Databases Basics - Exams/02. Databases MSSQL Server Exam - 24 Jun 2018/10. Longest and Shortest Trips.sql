SELECT a.Id AS [AccountId], 
       a.FirstName + ' ' + a.LastName AS [FullName], 
       MAX(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS [LongestTrip], 
       MIN(DATEDIFF(DAY, t.ArrivalDate, t.ReturnDate)) AS [ShortestTrip]
FROM Accounts AS a
     JOIN AccountsTrips AS acct ON acct.AccountId = a.Id
     JOIN Trips AS t ON t.Id = acct.TripId
WHERE a.MiddleName IS NULL
GROUP BY a.Id, 
         a.FirstName, 
         a.LastName
order by LongestTrip desc, a.Id