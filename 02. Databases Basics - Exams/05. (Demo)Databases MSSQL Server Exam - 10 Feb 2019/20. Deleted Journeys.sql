CREATE TABLE DeletedJourneys
(Id                     INT, 
 JourneyStart           DATETIME, 
 JourneyEnd             DATETIME, 
 Purpose                VARCHAR(11), 
 DestinationSpaceportId INT, 
 SpaceshipId            INT
)

GO

CREATE TRIGGER tr_OnDeleteJourneys ON Journeys
AFTER DELETE
AS
     BEGIN
         INSERT INTO DeletedJourneys
         (Id, 
          JourneyStart, 
          JourneyEnd, 
          Purpose, 
          DestinationSpaceportId, 
          SpaceshipId
         )
                SELECT Id, 
                       JourneyStart, 
                       JourneyEnd, 
                       Purpose, 
                       DestinationSpaceportId, 
                       SpaceshipId
                FROM deleted
     END

