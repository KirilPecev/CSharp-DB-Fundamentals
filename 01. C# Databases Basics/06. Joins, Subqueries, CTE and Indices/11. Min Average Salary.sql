SELECT MIN(AvgSalaries.AverageSalary)
FROM (
	SELECT AVG(e.Salary)  AS AverageSalary
	FROM Employees AS e
	GROUP BY e.DepartmentID
) AS AvgSalaries