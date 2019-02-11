CREATE PROCEDURE usp_ChangeJourneyPurpose
(@JourneyId  INT, 
 @NewPurpose VARCHAR(11)
)
AS
    BEGIN
        DECLARE @currentJourneyId INT=
        (
            SELECT Id
            FROM Journeys
            WHERE Id = @JourneyId
        )
        DECLARE @currentPurpose VARCHAR(11)=
        (
            SELECT Purpose
            FROM Journeys
            WHERE Id = @JourneyId
        )

        IF(@currentJourneyId IS NULL)
            BEGIN
                RAISERROR('The journey does not exist!', 16, 1)
                RETURN
        END
            ELSE
            IF(@currentPurpose = @NewPurpose)
                BEGIN
                    RAISERROR('You cannot change the purpose!', 16, 1)
                    RETURN
            END

        UPDATE Journeys
          SET 
              Purpose = @NewPurpose
        WHERE Id = @JourneyId
    END