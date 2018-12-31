SELECT m.MountainRange, p.PeakName, p.Elevation 
FROM Mountains AS m,
Peaks AS p
WHERE m.Id = p.MountainId AND m.MountainRange = 'Rila'
ORDER BY p.Elevation DESC