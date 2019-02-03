SELECT cu.CurrencyCode, 
       cu.[Description] AS [Currency], 
       COUNT(co.CountryCode) AS [NumberOfCountries]
FROM Currencies AS cu
     LEFT JOIN Countries AS co ON co.CurrencyCode = cu.CurrencyCode
GROUP BY cu.CurrencyCode, 
         cu.[Description]
ORDER BY NumberOfCountries DESC, 
         Currency