CREATE PROCEDURE usp_GetHoldersWithBalanceHigherThan(@number DECIMAL(18,2)) 
AS 
SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name]
  FROM AccountHolders AS ah
  JOIN Accounts AS acc 
  ON acc.AccountHolderId = ah.Id
  GROUP BY FirstName, LastName
  HAVING SUM(acc.Balance) > @number
  ORDER BY [First Name], [Last Name]	

 EXEC dbo.usp_GetHoldersWithBalanceHigherThan 200