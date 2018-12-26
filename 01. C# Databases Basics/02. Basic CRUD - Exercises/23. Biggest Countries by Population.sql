SELECT TOP(30) CountryName, [Population]
FROM Countries
WHERE Countries.ContinentCode IN 
(SELECT ContinentCode 
FROM Continents
WHERE ContinentName = 'Europe')
ORDER BY [Population] DESC, CountryName ASC