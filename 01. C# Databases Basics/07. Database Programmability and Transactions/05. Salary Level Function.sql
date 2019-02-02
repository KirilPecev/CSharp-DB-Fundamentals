CREATE FUNCTION ufn_GetSalaryLevel(@salary DECIMAL(18,4))
RETURNS NVARCHAR(8)
BEGIN
	DECLARE @result NVARCHAR(8);

	if @salary < 30000
	SET @result = 'Low';

	if @salary BETWEEN 30000 AND 50000
	SET @result = 'Average';
	
	if @salary>50000
	SET @result = 'High';

	RETURN @result;
END
GO

SELECT Salary,
		 dbo.ufn_GetSalaryLevel(Salary) AS [Salary Level]
FROM Employees