SELECT CASE
           WHEN DATEPART(QUARTER, Date) = 1
           THEN 'Q1'
           WHEN DATEPART(QUARTER, Date) = 2
           THEN 'Q2'
           WHEN DATEPART(QUARTER, Date) = 3
           THEN 'Q3'
           WHEN DATEPART(QUARTER, Date) = 4
           THEN 'Q4'
           ELSE 'TBA'
       END AS [Quarter], 
       s.[Name], 
       COUNT(se.StudentId) AS [StudentsCount]
FROM Exams AS e
     JOIN Subjects AS s ON s.Id = e.SubjectId
     JOIN StudentsExams AS se ON se.ExamId = e.Id
WHERE se.Grade >= 4.00
GROUP BY CASE
             WHEN DATEPART(QUARTER, Date) = 1
             THEN 'Q1'
             WHEN DATEPART(QUARTER, Date) = 2
             THEN 'Q2'
             WHEN DATEPART(QUARTER, Date) = 3
             THEN 'Q3'
             WHEN DATEPART(QUARTER, Date) = 4
             THEN 'Q4'
             ELSE 'TBA'
         END, 
         s.[Name]
ORDER BY Quarter