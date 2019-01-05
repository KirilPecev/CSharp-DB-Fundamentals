SELECT e.EmployeeID, e.FirstName, 
CASE 
   WHEN p.StartDate > '01/01/2005' THEN NULL
   ELSE p.NAME
  END 
FROM Employees AS e
JOIN EmployeesProjects AS ep
ON ep.EmployeeID = e.EmployeeID
JOIN Projects AS p
ON p.ProjectID = ep.ProjectID
WHERE e.EmployeeID = 24