CREATE TABLE Logs
(LogId     INT IDENTITY, 
 AccountId INT NOT NULL, 
 OldSum    DECIMAL(15, 2) NOT NULL, 
 NewSum    DECIMAL(15, 2) NOT NULL
                          CONSTRAINT PK_Log PRIMARY KEY(LogId)
)

GO

CREATE TRIGGER tr_EntryIntoLogsTable ON Accounts
AFTER UPDATE
AS
     INSERT INTO Logs (AccountId, OldSum, NewSum)
		SELECT i.Id, 
               d.Balance,
			   i.Balance
            FROM inserted  AS i 
			join deleted as d on i.Id = d.Id