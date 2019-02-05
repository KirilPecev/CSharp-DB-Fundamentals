CREATE PROCEDURE usp_MoveVehicle
(@vehicleId INT, 
 @officeId  INT
)
AS
    BEGIN
        --Get parking places in given office ID
        DECLARE @GivenOfficeParkingPlaces INT=
        (
            SELECT ParkingPlaces
            FROM Offices
            WHERE id = @officeId
        )

        --Get all parked cars in that office
        DECLARE @AllParkedCarsInGivenOffice INT=
        (
            SELECT COUNT(v.Id)
            FROM Vehicles AS v
                 JOIN Offices AS o ON o.Id = v.OfficeId
            WHERE OfficeId = @officeId
        )

        IF(@GivenOfficeParkingPlaces > @AllParkedCarsInGivenOffice)
            BEGIN
                UPDATE Vehicles
                  SET 
                      OfficeId = @officeId
                WHERE Id = @vehicleId
        END
            ELSE
            BEGIN
                RAISERROR('Not enough room in this office!', 16, 1)
                ROLLBACK
        END
    END