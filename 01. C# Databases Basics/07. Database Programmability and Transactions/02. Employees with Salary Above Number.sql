CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber @number DECIMAL(18,4) 
AS
	SELECT FirstName AS [First Name], LastName AS [Last Name]
	FROM Employees
	WHERE Salary>=@number

EXEC usp_GetEmployeesSalaryAboveNumber 48100