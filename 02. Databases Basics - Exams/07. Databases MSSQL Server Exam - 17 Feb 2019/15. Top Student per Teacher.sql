SELECT j.[Teacher Full Name], 
       j.SubjectName, 
       j.[Student Full Name], 
       FORMAT(j.TopGrade, 'N2') AS Grade
FROM
(
    SELECT k.[Teacher Full Name], 
           k.SubjectName, 
           k.[Student Full Name], 
           k.AverageGrade AS TopGrade, 
           ROW_NUMBER() OVER(PARTITION BY k.[Teacher Full Name]
           ORDER BY k.AverageGrade DESC) AS RowNumber
    FROM
    (
        SELECT CONCAT(t.FirstName, ' ', t.LastName) AS [Teacher Full Name], 
               su.[Name] AS [SubjectName], 
               CONCAT(s.FirstName, ' ', s.LastName) AS [Student Full Name], 
               AVG(ss.Grade) AS [AverageGrade]
        FROM Teachers AS t
             JOIN StudentsTeachers AS st ON st.TeacherId = t.Id
             JOIN Students AS s ON s.Id = st.StudentId
             JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
             JOIN Subjects AS su ON su.Id = ss.SubjectId
                                    AND su.Id = t.SubjectId
        GROUP BY t.FirstName, 
                 t.LastName, 
                 s.FirstName, 
                 s.LastName, 
                 su.[Name]
    ) AS k
) AS j
WHERE j.RowNumber = 1
ORDER BY j.SubjectName, 
         j.[Teacher Full Name], 
         TopGrade DESC