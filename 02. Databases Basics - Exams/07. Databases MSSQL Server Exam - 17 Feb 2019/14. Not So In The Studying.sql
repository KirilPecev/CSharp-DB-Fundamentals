SELECT s.FirstName + ISNULL(' ' + s.MiddleName, '') + ' ' + s.LastName AS [FullName]
FROM Students AS s
     LEFT JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
WHERE ss.SubjectId IS NULL
ORDER BY FullName