CREATE PROCEDURE usp_PlaceOrder
(@JobId        INT, 
 @SerialNumber VARCHAR(50), 
 @Quantity     INT
)
AS
    BEGIN
        IF(@Quantity <= 0)
            BEGIN
                THROW 50012, 'Part quantity must be more than zero!', 1
        END

        IF(
        (
            SELECT [Status]
            FROM Jobs
            WHERE JobId = @JobId
        ) = 'Finished')
            BEGIN
                THROW 50011, 'This job is not active!', 1
        END

		
		 IF(
        (
            SELECT JobId
            FROM Jobs
            WHERE JobId = @JobId
        ) IS NULL)
            BEGIN
                THROW 50013, 'Job not found!', 1
        END

        DECLARE @currentPartId INT=
        (
            SELECT PartId
            FROM Parts
            WHERE SerialNumber = @SerialNumber
        )

        IF(@currentPartId IS NULL)
            BEGIN
                THROW 50014, 'Part not found!', 1
        END

        DECLARE @currentJob INT=
        (
            SELECT JobId
            FROM Orders
            WHERE JobId = @JobId
                  AND IssueDate IS NULL
        )

        IF(@currentJob IS NULL)
            BEGIN
                INSERT INTO Orders
                (JobId, 
                 IssueDate, 
                 Delivered
                )
                VALUES
                (@JobId, 
                 NULL, 
                 0
                )
        END

        DECLARE @orderId INT=
        (
            SELECT TOP 1 OrderId
            FROM Orders
            WHERE JobId = @JobId
                  AND IssueDate IS NULL
        );

        DECLARE @isPartExist INT=
        (
            SELECT PartId
            FROM OrderParts
            WHERE PartId = @currentPartId
                  AND OrderId = @orderId
        )

        IF(@isPartExist IS NULL)
            BEGIN
                INSERT INTO OrderParts
                (OrderId, 
                 PartId, 
                 Quantity
                )
                VALUES
                (@orderId, 
                 @currentPartId, 
                 @Quantity
                )
        END
            ELSE
            BEGIN
                UPDATE OrderParts
                  SET 
                      Quantity+=@Quantity
                WHERE PartId = @currentPartId
                      AND OrderId = @orderId
        END
    END