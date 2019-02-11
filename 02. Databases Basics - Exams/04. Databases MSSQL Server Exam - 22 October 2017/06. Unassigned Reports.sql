--This dont work in Judge, but the result is true.
SELECT r.[Description], 
       r.OpenDate
FROM Reports AS r
     LEFT JOIN Employees AS e ON e.Id = r.EmployeeId
WHERE e.Id IS NULL
ORDER BY r.OpenDate, 
         r.[Description]

---------------------------------------------------------------

--This works in Judge.
SELECT [Description], 
       OpenDate
FROM Reports
WHERE EmployeeId IS NULL
ORDER BY OpenDate, 
         [Description]