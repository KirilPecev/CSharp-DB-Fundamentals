CREATE PROCEDURE usp_TransferMoney
(@SenderId   INT, 
 @ReceiverId INT, 
 @Amount     DECIMAL(15, 4)
)
AS
     DECLARE @CurrentSenderAccount INT=
     (
         SELECT Id
         FROM Accounts
         WHERE Id = @SenderId
     )

     DECLARE @CurrentReceiverAccount INT=
     (
         SELECT Id
         FROM Accounts
         WHERE Id = @ReceiverId
     )

     IF(@CurrentSenderAccount IS NOT NULL
        AND @CurrentReceiverAccount IS NOT NULL)
         BEGIN
             IF(@Amount > 0)
                 BEGIN
                     EXEC dbo.usp_WithdrawMoney 
                          @SenderId, 
                          @Amount

                     EXEC dbo.usp_DepositMoney 
                          @ReceiverId, 
                          @Amount
             END
     END