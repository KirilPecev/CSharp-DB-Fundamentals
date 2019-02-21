SELECT k.FullName, 
       k.Subjects, 
       COUNT(st.StudentId) AS [Students]
FROM
(
    SELECT t.Id, 
           CONCAT(t.FirstName, ' ', t.LastName) AS [FullName], 
           CONCAT(s.[Name], '-', SUM(s.Lessons)) AS Subjects
    FROM Teachers AS t
         JOIN Subjects AS s ON s.Id = t.SubjectId
    GROUP BY t.Id, 
             t.FirstName, 
             t.LastName, 
             s.Name
) AS k
JOIN StudentsTeachers AS st ON st.TeacherId = k.Id
GROUP BY k.FullName, 
         k.Subjects
ORDER BY Students DESC, 
         k.FullName, 
         k.Subjects