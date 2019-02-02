CREATE PROCEDURE usp_DepositMoney
 @AccountId   INT, 
 @MoneyAmount DECIMAL(15, 4)

AS
     DECLARE @CurrentAccount INT =
     (
         SELECT Id
         FROM Accounts
         WHERE Id = @AccountId
     )

     IF(@CurrentAccount IS NOT NULL)
         BEGIN
             IF(@MoneyAmount > 0)
                 BEGIN
                     UPDATE Accounts
                       SET 
                           Balance+=@MoneyAmount
                     WHERE Id = @AccountId
             END
     END
