SELECT DISTINCT 
       c.[Name] AS [Category Name]
FROM Categories AS c
     JOIN Reports AS r ON r.CategoryId = c.Id
     JOIN Users AS u ON u.Id = r.UserId
WHERE FORMAT(r.OpenDate, 'MM-dd') = FORMAT(u.BirthDate, 'MM-dd')
ORDER BY [Category Name]