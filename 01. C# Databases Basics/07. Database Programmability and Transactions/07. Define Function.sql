CREATE FUNCTION ufn_IsWordComprised(@setOfLetters NVARCHAR(50), @word NVARCHAR(50))
RETURNS BIT
BEGIN
	DECLARE @index INT = 1;
	
	WHILE(@index<=LEN(@word))
		BEGIN
			DECLARE @currentLetter NVARCHAR(1) = SUBSTRING(@word, @index, 1);
			IF CHARINDEX(@currentLetter, @setOfLetters) = 0
				RETURN 0;
			ELSE
				SET @index += 1;
		END

	RETURN 1;
END
GO

 SELECT dbo.ufn_IsWordComprised('oistmiahf', 'Sofia')