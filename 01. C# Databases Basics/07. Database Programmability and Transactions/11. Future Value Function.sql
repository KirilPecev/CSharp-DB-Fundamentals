CREATE OR ALTER FUNCTION ufn_CalculateFutureValue (@sum DECIMAL(12,4), @yearlyInterestRate FLOAT, @numberOfYears INT)
RETURNS DECIMAL(12,4)
	BEGIN
		DECLARE @result DECIMAL(12,4) = @sum*(POWER(1 + @yearlyInterestRate, @numberOfYears))
		RETURN @result
	END
GO

SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)