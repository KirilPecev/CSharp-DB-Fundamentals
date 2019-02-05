CREATE PROC usp_CancelOrder(@OrderId INT, @CancelDate DATETIME)
AS
	DECLARE @CurrentOrderDate DATETIME = (SELECT DateTime FROM Orders WHERE @OrderId = Id)

	IF(@CurrentOrderDate IS NULL)
	BEGIN
		RAISERROR('The order does not exist!',16,1)
	END

	IF(DATEDIFF(DAY,@CurrentOrderDate,@CancelDate) > 3)
	BEGIN
		RAISERROR('You cannot cancel the order!',16,1)
	END

	DELETE FROM OrderItems
		WHERE OrderId = @OrderId

	DELETE FROM Orders
		WHERE Id = @OrderId

