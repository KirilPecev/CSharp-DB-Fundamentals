CREATE PROCEDURE usp_CalculateFutureValueForAccount @accountID INT, @rate FLOAT
AS
	SELECT ah.Id,
		   ah.FirstName AS [First Name], 
		   ah.LastName AS [Last Name], 
		   acc.Balance AS [Current Balance],
		   dbo.ufn_CalculateFutureValue(acc.Balance, 0.1, 5) AS [Balance in 5 years]
	FROM AccountHolders AS ah
	JOIN Accounts AS acc
	ON ah.Id = acc.AccountHolderId AND acc.Id = @accountID
	