CREATE FUNCTION udf_GetReportsCount
(@employeeId INT, 
 @statusId   INT
)
RETURNS INT
AS
     BEGIN
         RETURN
         (
             SELECT COUNT(r.Id)
             FROM Reports AS r
             WHERE EmployeeId = @employeeId
                   AND r.StatusId = @statusId
         )
     END

