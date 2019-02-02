CREATE PROCEDURE usp_EmployeesBySalaryLevel @salaryLevel NVARCHAR(8)
AS
	DECLARE @currentSalaryLevel NVARCHAR(8);
	SELECT FirstName AS [First Name], LastName AS [Last Name]
	FROM Employees
	WHERE dbo.ufn_GetSalaryLevel(Salary) = 'High'


EXEC usp_EmployeesBySalaryLevel 'High'