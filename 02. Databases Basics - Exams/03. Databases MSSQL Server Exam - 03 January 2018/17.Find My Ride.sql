CREATE FUNCTION udf_CheckForVehicle
(@townName    NVARCHAR(50), 
 @seatsNumber INT
)
RETURNS NVARCHAR(50)
AS
     BEGIN
         DECLARE @Cars TABLE
         (Id         INT, 
          OfficeName NVARCHAR(50), 
          Model      NVARCHAR(50)
         )

         INSERT INTO @Cars
         (Id, 
          OfficeName, 
          Model
         )
                SELECT v.Id, 
                       o.[Name], 
                       m.Model
                FROM Vehicles AS v
                     JOIN Offices AS o ON o.Id = v.OfficeId
                     JOIN Towns AS t ON t.Id = o.TownId
                     JOIN Models AS m ON m.Id = v.ModelId
                WHERE m.Seats = @seatsNumber
                      AND t.[Name] = @townName

         DECLARE @CurrentCarId INT=
         (
             SELECT TOP (1) Id
             FROM @Cars
             ORDER BY OfficeName
         )

         DECLARE @OfficeName NVARCHAR(50)=
         (
             SELECT TOP (1) OfficeName
             FROM @Cars
			  ORDER BY OfficeName
         )

         DECLARE @Model NVARCHAR(50)=
         (
             SELECT TOP (1) Model
             FROM @Cars
			  ORDER BY OfficeName
         )

         IF(@CurrentCarId IS NULL)
             BEGIN
                 RETURN 'NO SUCH VEHICLE FOUND'
         END

         RETURN CONCAT(@OfficeName, ' - ', @Model)
     END