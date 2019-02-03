SELECT p.PeakName, 
       m.MountainRange, 
       c.CountryName, 
       co.ContinentName
FROM Peaks AS p
     JOIN Mountains AS m ON m.Id = p.MountainId
     JOIN MountainsCountries AS mc ON mc.MountainId = m.Id
     JOIN Countries AS c ON c.CountryCode = mc.CountryCode
     JOIN Continents AS co ON co.ContinentCode = c.ContinentCode
ORDER BY p.PeakName, 
         c.CountryName