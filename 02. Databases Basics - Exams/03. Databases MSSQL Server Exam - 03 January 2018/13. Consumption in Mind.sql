DECLARE @Ordered TABLE
(Model      NVARCHAR(50), 
 [Count]    INT, 
 AVGConsump DECIMAL(14, 6)
)

INSERT INTO @Ordered
       SELECT m.Model, 
              COUNT(o.Id) AS TimesOrdered, 
              AVG(m.Consumption) AS AverageConsumption
       FROM Models AS m
            JOIN Vehicles AS v ON v.ModelId = m.Id
            JOIN Orders AS o ON o.VehicleId = v.Id
       GROUP BY m.Model
       HAVING AVG(m.Consumption) BETWEEN 5 AND 15

SELECT TOP (3) m.Manufacturer, 
               CAST(o.AVGConsump AS DECIMAL(14, 6))
FROM @Ordered AS o
     JOIN Models AS m ON m.Model = o.Model
ORDER BY [Count] DESC