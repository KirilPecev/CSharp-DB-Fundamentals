SELECT c.ContinentName, 
       SUM(CAST(co.AreaInSqKm AS BIGINT)) AS [CountriesArea], 
       SUM(CAST(co.Population AS BIGINT)) AS [CountriesPopulation]
FROM Continents AS c
     JOIN Countries AS co ON co.ContinentCode = c.ContinentCode
GROUP BY c.ContinentName
ORDER BY CountriesPopulation DESC