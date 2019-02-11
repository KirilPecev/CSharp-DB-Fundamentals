--this also works, but not in Judge
CREATE TRIGGER tr_ChangeStatusId ON Reports
AFTER UPDATE
AS
     BEGIN
         DECLARE @id INT=
         (
             SELECT Id
             FROM deleted
         )

         DECLARE @oldValue DATETIME=
         (
             SELECT CloseDate
             FROM deleted
         )

         DECLARE @newValue DATETIME=
         (
             SELECT CloseDate
             FROM inserted
         )

         IF(@oldValue IS NULL
            AND @newValue IS NOT NULL)
             BEGIN
                 UPDATE Reports
                   SET 
                       StatusId =
                 (
                     SELECT Id
                     FROM [Status]
                     WHERE Label = 'completed'
                 )
                 WHERE Id = @id
         END
     END

------------------------------------------------------------------

-- this solution works in Judge
CREATE TRIGGER tr_ChangeStatusId ON Reports
AFTER UPDATE
AS
BEGIN
	UPDATE Reports
	SET StatusId = (SELECT Id FROM Status WHERE Label = 'completed')
	WHERE Id IN (SELECT Id FROM inserted
			     WHERE Id IN (SELECT Id FROM deleted
						      WHERE CloseDate IS NULL)
			           AND CloseDate IS NOT NULL)   
END;