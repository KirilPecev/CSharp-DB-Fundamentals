SELECT c.CountryName, 
       co.ContinentName, 
       COUNT(cr.RiverId) AS [RiversCount], 
       IIF(SUM(r.[Length]) IS NULL, 0, SUM(r.[Length])) AS [TotalLength]
FROM Countries AS c
     LEFT JOIN Continents AS co ON co.ContinentCode = c.ContinentCode
     LEFT JOIN CountriesRivers AS cr ON cr.CountryCode = c.CountryCode
     LEFT JOIN Rivers AS r ON r.Id = cr.RiverId
GROUP BY c.CountryName, 
         co.ContinentName
ORDER BY RiversCount DESC, 
         TotalLength DESC, 
         c.CountryName