SELECT TOP (5) e.EmployeeID, e.JobTitle, e.AddressID, a.AddressText
FROM Employees AS e,
Addresses AS a
WHERE e.AddressID = a.AddressID
ORDER BY e.AddressID