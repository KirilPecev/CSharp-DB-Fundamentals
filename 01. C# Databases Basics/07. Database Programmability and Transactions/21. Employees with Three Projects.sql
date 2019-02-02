CREATE PROCEDURE usp_AssignProject(@emloyeeId INT, @projectID INT)
AS
	DECLARE @totalProjects INT = (
		SELECT COUNT(ProjectID)
		FROM EmployeesProjects
		WHERE EmployeeID = @emloyeeId)

	IF @totalProjects>=3
	BEGIN	
		RAISERROR('The employee has too many projects!', 16, 1)
		ROLLBACK
	END
	ELSE
	BEGIN
		INSERT INTO EmployeesProjects (EmployeeID, ProjectID)
		VALUES
		(@emloyeeId, @projectID)
	END	
GO

EXEC usp_AssignProject 2,2			