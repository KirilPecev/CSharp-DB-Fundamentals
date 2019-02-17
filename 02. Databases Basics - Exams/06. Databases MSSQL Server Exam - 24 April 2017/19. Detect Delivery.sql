CREATE TRIGGER tr_OnChangeDeliveryStatus ON Orders
AFTER UPDATE
AS
     BEGIN
         DECLARE @oldValue BIT=
         (
             SELECT Delivered
             FROM deleted
         )

         DECLARE @newValue BIT=
         (
             SELECT Delivered
             FROM inserted
         )

         IF(@oldValue = 0
            AND @newValue = 1)
             BEGIN
                 UPDATE Parts
                   SET 
                       StockQty+=k.Quantity
                 FROM
                 (
                     SELECT op.Quantity AS [Quantity], 
                            op.PartId
                     FROM OrderParts AS op
                          JOIN inserted AS i ON i.OrderId = op.OrderId
                 ) AS k
                 WHERE Parts.PartId = k.PartId
         END
     END