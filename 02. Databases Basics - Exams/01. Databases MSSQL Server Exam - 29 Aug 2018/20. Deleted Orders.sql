CREATE TABLE DeletedOrders(
	OrderId INT,
	ItemId INT,
	ItemQuantity INT
)

GO

CREATE TRIGGER t_DeleteOrders ON OrderItems 
AFTER DELETE
 AS
	INSERT INTO DeletedOrders(OrderId, ItemId, ItemQuantity)
	SELECT OrderId, ItemId, Quantity
		FROM deleted 