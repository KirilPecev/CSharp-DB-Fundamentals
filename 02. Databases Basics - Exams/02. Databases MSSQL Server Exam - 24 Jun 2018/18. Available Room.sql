CREATE FUNCTION udf_GetAvailableRoom
(@HotelId INT, 
 @Date    DATE, 
 @People  INT
)
RETURNS NVARCHAR(MAX)
AS
     BEGIN
         DECLARE @BookedRooms TABLE(id INT)
         INSERT INTO @BookedRooms
                SELECT DISTINCT 
                       r.Id
                FROM Rooms AS r
                     LEFT JOIN Trips AS t ON t.RoomId = r.Id
                WHERE r.HotelId = @HotelId
                      AND @Date BETWEEN t.ArrivalDate AND t.ReturnDate
                      AND t.CancelDate IS NULL

         DECLARE @Rooms TABLE
         (Id         INT, 
          Price      DECIMAL(18, 2), 
          Type       NVARCHAR(20), 
          Beds       INT, 
          TotalPrice DECIMAL(18, 2)
         )
         INSERT INTO @Rooms
                SELECT TOP 1 r.Id, 
                             r.Price, 
                             r.Type, 
                             r.Beds, 
                             (h.BaseRate + r.Price) * @People AS [TotalPrice]
                FROM Rooms AS r
                     LEFT JOIN Hotels AS h ON h.Id = r.HotelId
                WHERE r.HotelId = @HotelId
                      AND r.Beds >= @People
                      AND r.Id NOT IN
                (
                    SELECT Id
                    FROM @BookedRooms
                )
                ORDER BY TotalPrice DESC

         DECLARE @RoomCount INT=
         (
             SELECT COUNT(*)
             FROM @Rooms
         )

         IF(@RoomCount < 1)
             BEGIN
                 RETURN 'No rooms available'
         END

         DECLARE @Result NVARCHAR(MAX)=
         (
             SELECT CONCAT('Room ', Id, ': ', Type, ' (', Beds, ' beds) - ', '$', TotalPrice)
             FROM @Rooms
         )

         RETURN @Result
     END