SELECT Department, 
       Category, 
       [Percentage]
FROM
(
    SELECT d.[Name] AS Department, 
           c.[Name] AS Category, 
           CAST(ROUND(COUNT(*) OVER(PARTITION BY c.Id) * 100.00 / COUNT(*) OVER(PARTITION BY d.Id), 0) AS INT) AS Percentage
    FROM Categories AS c
         JOIN Reports AS r ON r.Categoryid = c.Id
         JOIN Departments AS d ON d.Id = c.Departmentid
) AS s
GROUP BY Department, 
         Category, 
         [Percentage]
ORDER BY Department, 
         Category, 
         [Percentage]