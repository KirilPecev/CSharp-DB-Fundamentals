SELECT c.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Countries AS c
JOIN MountainsCountries AS mc
ON mc.CountryCode = c.CountryCode
JOIN Mountains as m
ON m.Id=mc.MountainId
JOIN Peaks as p
ON p.MountainId = mc.MountainId
WHERE c.CountryName = 'Bulgaria' AND p.Elevation>2835
ORDER BY p.Elevation DESC