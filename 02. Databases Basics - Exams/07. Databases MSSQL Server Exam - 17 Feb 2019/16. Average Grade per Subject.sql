SELECT s.[Name], 
       AVG(ss.Grade)
FROM StudentsSubjects AS ss
     JOIN Subjects AS s ON ss.SubjectId = s.Id
GROUP BY s.[Name], 
         s.Id
ORDER BY s.Id