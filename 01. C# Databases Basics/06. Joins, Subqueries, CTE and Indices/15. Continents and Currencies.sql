SELECT TableOne.ContinentCode, TableOne.CurrencyCode, TableOne.CurrencyUsage 
FROM 
(
	SELECT c.ContinentCode ,c.CurrencyCode, COUNT(*) AS 'CurrencyUsage'
	FROM Countries AS c
	WHERE c.CurrencyCode IS NOT NULL
	GROUP BY c.ContinentCode ,c.CurrencyCode
	HAVING COUNT(c.CurrencyCode) > 1
) AS TableOne
INNER JOIN 
(SELECT currencies.ContinentCode, MAX(currencies.CurrencyUsage) AS MaxUsage 
FROM 
		(
		SELECT c.ContinentCode ,c.CurrencyCode, COUNT(*) AS 'CurrencyUsage'
		FROM Countries AS c
		WHERE c.CurrencyCode IS NOT NULL
		GROUP BY c.ContinentCode ,c.CurrencyCode
		HAVING COUNT(c.CurrencyCode) > 1
		) AS currencies
GROUP BY currencies.ContinentCode
) AS TableTwo
ON TableOne.ContinentCode = TableTwo.ContinentCode AND TableOne.CurrencyUsage = TableTwo.MaxUsage
ORDER BY TableOne.ContinentCode