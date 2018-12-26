SELECT CountryName AS [Country Name], IsoCode AS [ISO Code]
FROM Countries
WHERE UPPER(CountryName) LIKE '%A%A%A%'
ORDER BY IsoCode