CREATE FUNCTION udf_GetPromotedProducts(@CurrentDate DATETIME, @StartDate DATETIME, @EndDate DATETIME, @Discount INT, @FirstItemId INT, @SecondItemId INT, @ThirdItemId INT)
RETURNS NVARCHAR(80)
AS
BEGIN
		DECLARE @FirstItemPrice DECIMAL(18,2) = (SELECT Price FROM Items WHERE Id = @FirstItemId)
		DECLARE @SecondItemPrice DECIMAL(18,2) = (SELECT Price FROM Items WHERE Id = @SecondItemId)
		DECLARE @ThirdItemPrice DECIMAL(18,2) = (SELECT Price FROM Items WHERE Id = @ThirdItemId)

		IF(@FirstItemPrice IS NULL OR @SecondItemPrice IS NULL OR @ThirdItemPrice IS NULL)
		BEGIN
			RETURN 'One of the items does not exists!'
		END

		IF (@CurrentDate NOT BETWEEN @StartDate AND @EndDate)
		BEGIN
		 RETURN 'The current date is not within the promotion dates!'
		END

		DECLARE @NewFirstItemPrice DECIMAL(18,2) = @FirstItemPrice - (@FirstItemPrice * @Discount / 100)
		DECLARE @NewSecondItemPrice DECIMAL(18,2) = @SecondItemPrice - (@SecondItemPrice * @Discount / 100)
		DECLARE @NewThirdItemPrice DECIMAL(18,2) = @ThirdItemPrice - (@ThirdItemPrice * @Discount / 100)

		DECLARE @FirstItemName NVARCHAR(50) = (SELECT [Name] FROM Items WHERE Id = @FirstItemId)
	    DECLARE @SecondItemName NVARCHAR(50) = (SELECT [Name] FROM Items WHERE Id = @SecondItemId)
	    DECLARE @ThirdItemName NVARCHAR(50) = (SELECT [Name] FROM Items WHERE Id = @ThirdItemId)
	    
	    RETURN @FirstItemName + ' price: ' + CAST(ROUND(@NewFirstItemPrice,2) AS nvarchar) + ' <-> ' +
		       @SecondItemName + ' price: ' + CAST(ROUND(@NewSecondItemPrice,2) AS nvarchar)+ ' <-> ' +
		       @ThirdItemName + ' price: ' + CAST(ROUND(@NewThirdItemPrice,2) AS nvarchar)
			
END

SELECT dbo.udf_GetPromotedProducts('2018-08-02', '2018-08-01', '2018-08-03',13, 3,4,5)

SELECT dbo.udf_GetPromotedProducts('2018-08-01', '2018-08-02', '2018-08-03',13,3 ,4,5)