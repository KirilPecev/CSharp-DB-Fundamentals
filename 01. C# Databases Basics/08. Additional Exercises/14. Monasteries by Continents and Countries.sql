UPDATE Countries
  SET 
      CountryName = 'Burma'
WHERE CountryName = 'Myanmar'

INSERT INTO Monasteries
([Name], 
 CountryCode
)
VALUES
('Hanga Abbey', 
(
    SELECT CountryCode
    FROM Countries
    WHERE CountryName = 'Tanzania'
)
),
('Myin-Tin-Daik', 
(
    SELECT CountryCode
    FROM Countries
    WHERE CountryName = 'Myanmar'
)
)

SELECT co.ContinentName, 
       c.CountryName as [CountryName], 
       COUNT(m.Id) AS [MonasteriesCount]
FROM Continents AS co
     LEFT JOIN Countries AS c ON c.ContinentCode = co.ContinentCode and c.IsDeleted = 0
     LEFT JOIN Monasteries AS m ON m.CountryCode = c.CountryCode
GROUP BY co.ContinentName,
         c.CountryName
ORDER BY MonasteriesCount DESC, 
         CountryName