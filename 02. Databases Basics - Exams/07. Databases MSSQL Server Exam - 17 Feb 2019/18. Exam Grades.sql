CREATE FUNCTION udf_ExamGradesToUpdate
(@studentId INT, 
 @grade     DECIMAL(15, 2)
)
RETURNS NVARCHAR(MAX)
AS
     BEGIN
         DECLARE @currentStudentFirstName NVARCHAR(30)=
         (
             SELECT FirstName
             FROM Students
             WHERE Id = @studentId
         )

         IF(@currentStudentFirstName IS NULL)
             BEGIN
                 RETURN 'The student with provided id does not exist in the school!'
         END

         IF(@grade > 6.00)
             BEGIN
                 RETURN 'Grade cannot be above 6.00!'
         END

         DECLARE @newGrade DECIMAL(15, 2)= @grade + 0.50

         DECLARE @countOfGrades INT=
         (
             SELECT COUNT(StudentId)
             FROM StudentsExams
             WHERE Grade BETWEEN @grade AND @newGrade
                   AND StudentId = @studentId
         )

         RETURN 'You have to update ' + CAST(@countOfGrades AS nvarchar(max)) + ' grades for the student ' + @currentStudentFirstName
     END