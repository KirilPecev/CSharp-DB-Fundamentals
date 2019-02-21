CREATE TABLE ExcludedStudents
(StudentId   INT, 
 StudentName NVARCHAR(30)
)

GO

CREATE TRIGGER tr_OnExludeStudents ON Students
AFTER DELETE
AS
     BEGIN
         INSERT INTO ExcludedStudents
         (StudentId, 
          StudentName
         )
                SELECT Id, 
                       FirstName + ' ' + LastName
                FROM deleted
     END