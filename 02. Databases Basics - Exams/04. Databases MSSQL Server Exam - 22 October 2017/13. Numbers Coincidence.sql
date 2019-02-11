SELECT DISTINCT 
       u.Username
FROM Users AS u
     JOIN Reports AS r ON r.UserId = u.Id
     JOIN Categories AS c ON c.Id = r.CategoryId
WHERE LEFT(Username, 1) IN('1', '2', '3', '4', '5', '6', '7', '8', '9')
     AND CAST(c.Id AS VARCHAR) = LEFT(Username, 1)
     OR RIGHT(Username, 1) IN('1', '2', '3', '4', '5', '6', '7', '8', '9')
AND CAST(c.Id AS VARCHAR) = RIGHT(Username, 1)
ORDER BY u.Username