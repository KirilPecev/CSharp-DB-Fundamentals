SELECT DISTINCT 
       FirstName + ' ' + LastName AS [Full Name]
FROM Students AS s
     JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
     JOIN Subjects AS su ON su.Id = ss.SubjectId
     JOIN Exams AS e ON e.SubjectId = su.Id
     LEFT JOIN StudentsExams AS se ON se.StudentId = s.Id
WHERE se.StudentId IS NULL
ORDER BY [Full Name]