CREATE PROCEDURE usp_SwitchRoom
(@TripId       INT, 
 @TargetRoomId INT
)
AS
    BEGIN
        DECLARE @SourceHotelId INT=
        (
            SELECT h.Id
            FROM Hotels AS h
                 JOIN Rooms AS r ON r.HotelId = h.Id
                 JOIN Trips AS t ON t.RoomId = r.Id
            WHERE t.Id = @TripId
        )

        DECLARE @CurrentHotelId INT=
        (
            SELECT h.Id
            FROM Hotels AS h
                 JOIN Rooms AS r ON r.HotelId = h.Id
            WHERE r.Id = @TargetRoomId
        )

        IF(@SourceHotelId != @CurrentHotelId)
            BEGIN
                RAISERROR('Target room is in another hotel!', 16, 1)
        END

        DECLARE @PeopleCount INT=
        (
            SELECT COUNT(*)
            FROM AccountsTrips
            WHERE TripId = @TripId
        )

        DECLARE @CurrentRoomBeds INT=
        (
            SELECT Beds
            FROM Rooms
            WHERE Id = @TargetRoomId
        )

        IF(@PeopleCount > @CurrentRoomBeds)
            BEGIN
                RAISERROR('Not enough beds in target room!', 16, 1)
        END

        UPDATE Trips
          SET 
              RoomId = @TargetRoomId
        WHERE Id = @TripId
    END




