CREATE PROCEDURE usp_AssignEmployeeToReport
(@employeeId INT, 
 @reportId   INT
)
AS
    BEGIN
        DECLARE @id INT=
        (
            SELECT EmployeeId
            FROM Employees AS e
                 JOIN Reports AS r ON r.EmployeeId = e.Id
                 JOIN Categories AS c ON c.Id = r.CategoryId
            WHERE e.DepartmentId = c.DepartmentId
                  AND e.Id = @employeeId
        )

        IF(@id IS NULL)
            BEGIN
                RAISERROR('Employee doesn''t belong to the appropriate department!', 16, 1)
                ROLLBACK
        END
            ELSE
            BEGIN
                UPDATE Reports
                  SET 
                      EmployeeId = @employeeId
                WHERE Id = @reportId
        END
    END