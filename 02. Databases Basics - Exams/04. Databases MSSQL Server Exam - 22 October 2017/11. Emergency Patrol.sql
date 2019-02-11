SELECT r.OpenDate, 
       r.Description, 
       u.Email AS [Reportel Email]
FROM Reports AS r
     JOIN Categories AS c ON c.Id = r.CategoryId
     JOIN Departments AS d ON d.Id = c.DepartmentId
     JOIN Users AS u ON u.Id = r.UserId
WHERE r.CloseDate IS NULL
      AND LEN([Description]) > 20
	  and CHARINDEX('str',r.[Description]) > 0
      AND d.[Name] IN('Infrastructure', 'Emergency', 'Roads Maintenance')
ORDER BY r.OpenDate, 
         u.Email, 
         r.Id