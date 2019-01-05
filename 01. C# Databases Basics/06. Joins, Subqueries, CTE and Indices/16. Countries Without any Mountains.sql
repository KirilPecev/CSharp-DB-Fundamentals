SELECT COUNT(c.CountryCode) AS [CountryCode]
FROM Countries AS c
LEFT JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
WHERE mc.MountainId IS NULL