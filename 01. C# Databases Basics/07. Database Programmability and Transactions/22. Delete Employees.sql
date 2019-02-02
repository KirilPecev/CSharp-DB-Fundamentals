CREATE TABLE Deleted_Employees(
	EmployeeId INT NOT NULL, 
	FirstName NVARCHAR(50) NOT NULL, 
	LastName NVARCHAR(50) NOT NULL, 
	MiddleName NVARCHAR(50),  
	JobTitle NVARCHAR(50) NOT NULL, 
	DepartmentId INT NOT NULL, 
	Salary DECIMAL(18,2)
)

CREATE TRIGGER tr_Delete ON Employees
AFTER DELETE
AS
BEGIN
	INSERT INTO Deleted_Employees
	SELECT EmployeeID, FirstName, LastName, MiddleName, JobTitle, DepartmentID, Salary
	FROM deleted
END

DELETE FROM Employees
WHERE EmployeeID = 17

DELETE FROM EmployeesProjects
WHERE EmployeeID = 17
	 