SELECT TOP 10 s.FirstName, 
              s.LastName, 
              CAST(AVG(Grade) AS DECIMAL(15, 2)) AS Grade
FROM StudentsExams AS se
     JOIN Students AS s ON s.Id = se.StudentId
GROUP BY s.FirstName, 
         s.LastName
ORDER BY grade DESC, 
         s.FirstName, 
         s.LastName