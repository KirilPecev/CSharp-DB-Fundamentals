SELECT FirstName, 
       LastName, 
       COUNT(TeacherId) AS [TeachersCount]
FROM Students AS s
     JOIN StudentsTeachers AS st ON st.StudentId = s.Id
GROUP BY FirstName, 
         LastName