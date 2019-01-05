SELECT TOP (50) e.FirstName, e.LastName, t.Name, a.AddressText
FROM Employees AS e,
Addresses AS a,
Towns AS t
WHERE e.AddressID = a.AddressID AND a.TownID = t.TownID
ORDER BY e.FirstName, e.LastName