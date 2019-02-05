SELECT CASE
           WHEN YEAR(c.BirthDate) BETWEEN 1970 AND 1979 THEN '70' + char(39) + 's'
		   WHEN YEAR(c.BirthDate) BETWEEN 1980 AND 1989  THEN '80' + char(39) + 's'
		   WHEN YEAR(c.BirthDate) BETWEEN 1990 AND 1999  THEN '90' + char(39) + 's'
		   else 'Others'
       END AS [AgeGroup],
	   Sum(o.Bill) as [Revenue],
	   AVG(o.TotalMileage) as [AverageMileage]
FROM Clients AS c
     JOIN Orders AS o ON o.ClientId = c.Id
group by CASE
           WHEN YEAR(c.BirthDate) BETWEEN 1970 AND 1979 THEN '70' + char(39) + 's'
		   WHEN YEAR(c.BirthDate) BETWEEN 1980 AND 1989  THEN '80' + char(39) + 's'
		   WHEN YEAR(c.BirthDate) BETWEEN 1990 AND 1999  THEN '90' + char(39) + 's'
		   else 'Others'
       END 
order by AgeGroup