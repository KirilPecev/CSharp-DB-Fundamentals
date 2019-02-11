SELECT concat(e.FirstName, ' ', e.LastName) AS [Name], 
       concat(ISNULL(CDC.ReportCount, 0), '/', ISNULL(odc.ReportCount, 0)) AS [Close Open Reports]
FROM Employees AS e
     JOIN
(
    SELECT EmployeeId, 
           COUNT(*) AS [ReportCount]
    FROM Reports AS r
    WHERE YEAR(OpenDate) = 2016
    GROUP BY EmployeeId
) AS ODC ON odc.EmployeeId = e.Id
     LEFT JOIN
(
    SELECT EmployeeId, 
           COUNT(*) AS [ReportCount]
    FROM Reports AS r
    WHERE YEAR(CloseDate) = 2016
    GROUP BY EmployeeId
) AS CDC ON CDC.EmployeeId = e.Id
ORDER BY [Name], 
         e.Id