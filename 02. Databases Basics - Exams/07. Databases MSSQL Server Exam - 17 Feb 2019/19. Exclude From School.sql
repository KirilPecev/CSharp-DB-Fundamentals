CREATE PROCEDURE usp_ExcludeFromSchool(@StudentId INT)
AS
    BEGIN
        DECLARE @currentStudent INT=
        (
            SELECT Id
            FROM Students
            WHERE id = @StudentId
        )

        IF(@currentStudent IS NULL)
            BEGIN
                RAISERROR('This school has no student with the provided id!', 16, 1)
        END
            ELSE
            BEGIN

                DELETE FROM StudentsTeachers
                WHERE StudentId = @StudentId

                DELETE FROM StudentsExams
                WHERE StudentId = @StudentId

                DELETE FROM StudentsSubjects
                WHERE StudentId = @StudentId

                DELETE FROM Students
                WHERE Id = @StudentId
        END
    END


EXEC usp_ExcludeFromSchool 301
