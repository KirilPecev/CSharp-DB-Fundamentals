CREATE PROCEDURE usp_GetTownsStartingWith @String NVARCHAR(50)
AS
	SELECT Name AS [Town]
	FROM Towns
	WHERE SUBSTRING(Name,1,LEN(@String)) = @String

EXEC usp_GetTownsStartingWith bo
