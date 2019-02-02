CREATE TABLE NotificationEmails
(Id        INT IDENTITY PRIMARY KEY, 
 Recipient INT NOT NULL, 
 [Subject] NVARCHAR(50) NOT NULL, 
 Body      NVARCHAR(MAX) NOT NULL
)

GO

CREATE TRIGGER tr_EmailLogs ON Logs
AFTER INSERT
AS
     declare @accountId int = (select i.AccountId from inserted as i)
	 declare @date datetime = getdate()
	 declare @oldSum decimal(15,2) = (select OldSum from Logs where AccountId = @accountId)
	 declare @newSum decimal(15,2) = (select NewSum from Logs where AccountId = @accountId)
     
	 INSERT INTO NotificationEmails(Recipient, [Subject], Body)
	 values(@accountId,CONCAT('Balance change for account: ', @accountId),CONCAT('On ', @date, ' your balance was changed from ', @oldSum, ' to ', @newSum, '.') )



update Accounts
set Balance  = 200
where Id = 3