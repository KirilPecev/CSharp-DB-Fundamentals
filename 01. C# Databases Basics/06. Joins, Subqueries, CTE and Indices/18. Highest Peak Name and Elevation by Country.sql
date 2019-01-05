SELECT TOP(5)
	c.CountryName, 
	ISNULL(p.PeakName, '(no highest peak)') AS 'HighestPeakName', 
	ISNULL(MAX(p.Elevation), 0) AS 'HighestPeakElevation', 
	ISNULL(m.MountainRange, '(no mountain)') AS [Mountain]
FROM Countries AS c
	LEFT JOIN MountainsCountries AS mc
	ON mc.CountryCode = c.CountryCode
	LEFT JOIN Peaks AS p
	ON p.MountainId = mc.MountainId
	LEFT JOIN Mountains AS m
	ON m.Id = mc.MountainId
GROUP BY c.CountryName, p.PeakName, m.MountainRange
ORDER BY c.CountryName, p.PeakName
