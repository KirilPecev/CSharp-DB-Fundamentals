CREATE FUNCTION ufn_CashInUsersGames(@gameName NVARCHAR(50))
RETURNS @returnetTable TABLE
(
	SumCash DECIMAL(18,4)
)
AS
BEGIN
	DECLARE @result DECIMAL(18,4) = (
		SELECT SUM(ug.Cash) AS [Cash]
		FROM (
			SELECT Cash, 
				   GameId, 
			       ROW_NUMBER() OVER (ORDER BY CASH DESC) AS [RowNumber]
			FROM UsersGames
			WHERE GameId = (
				SELECT Id
				FROM Games
				WHERE Name = @gameName)
			) AS ug
	WHERE ug.RowNumber % 2 != 0
	)
	
	INSERT INTO @returnetTable
		SELECT @result 
	RETURN		
END
GO

SELECT *
FROM ufn_CashInUsersGames('Lily Stargazer')
