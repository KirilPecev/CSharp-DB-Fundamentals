SELECT DISTINCT 
       k.FirstName, 
       k.LastName, 
       k.Grade
FROM
(
    SELECT s.FirstName, 
           s.LastName, 
           ROW_NUMBER() OVER(PARTITION BY s.FirstName, 
                                          s.LastName
           ORDER BY ss.Grade DESC) AS ranked, 
           ss.SubjectId, 
           ss.Grade
    FROM Students AS s
         JOIN StudentsSubjects AS ss ON ss.StudentId = s.Id
) AS k
JOIN StudentsSubjects AS ss ON ss.StudentId = k.SubjectId
WHERE k.ranked = 2
ORDER BY k.FirstName, 
         k.LastName

------------------------------------------------------------------

SELECT k.FirstName, 
       k.LastName, 
       k.Grade
FROM
(
    SELECT s.FirstName, 
           s.LastName, 
           ss.Grade, 
           ROW_NUMBER() OVER(PARTITION BY s.FirstName, 
                                          s.LastName
           ORDER BY ss.Grade DESC) AS ranked
    FROM StudentsSubjects AS ss
         JOIN Students AS s ON s.Id = ss.StudentId
) AS k
WHERE k.ranked = 2
ORDER BY k.FirstName, 
         k.LastName