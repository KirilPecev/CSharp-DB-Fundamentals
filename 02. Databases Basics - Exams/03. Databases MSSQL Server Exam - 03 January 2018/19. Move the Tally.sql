CREATE TRIGGER tr_addMileage ON Orders
AFTER UPDATE
AS
     DECLARE @VehicleId INT=
     (
         SELECT VehicleId
         FROM inserted
     )

     DECLARE @NewValue INT=
     (
         SELECT TotalMileage
         FROM inserted
     )

     DECLARE @OldValue INT=
     (
         SELECT TotalMileage
         FROM deleted
     )

     IF(@OldValue IS NULL)
         BEGIN
             UPDATE Vehicles
               SET 
                   Mileage+=@NewValue
             WHERE id = @VehicleId
     END