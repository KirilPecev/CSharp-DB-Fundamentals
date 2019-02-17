CREATE FUNCTION udf_GetCost
(@JobId INT
)
RETURNS DECIMAL(8, 2)
AS
     BEGIN
         RETURN
         (
             SELECT ISNULL(SUM(p.Price * op.Quantity), 0)
             FROM Jobs AS j
                  JOIN Orders AS o ON o.JobId = j.JobId
                  JOIN OrderParts AS op ON op.OrderId = o.OrderId
                  JOIN Parts AS p ON p.PartId = op.PartId
             WHERE j.JobId = @JobId
         )
     END