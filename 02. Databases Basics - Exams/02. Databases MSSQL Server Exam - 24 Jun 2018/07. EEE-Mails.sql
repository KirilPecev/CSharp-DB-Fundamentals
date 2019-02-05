SELECT FirstName, 
       LastName, 
       FORMAT(BirthDate, 'MM-dd-yyyy'), 
       c.Name AS [Hometown], 
       Email
FROM Accounts AS a
     JOIN Cities AS c ON c.Id = a.CityId
WHERE LEFT(Email, 1) = 'e'
ORDER BY Hometown DESC