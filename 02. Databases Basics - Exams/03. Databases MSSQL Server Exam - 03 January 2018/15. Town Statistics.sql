WITH t
     AS (SELECT t.Id, 
                t.[Name] AS [TownName], 
                COUNT(CASE
                          WHEN c.Gender = 'm'
                          THEN 1
                      END) AS Male, 
                COUNT(CASE
                          WHEN c.Gender = 'f'
                          THEN 1
                      END) AS Female, 
                COUNT(*) AS Total
         FROM Towns AS t
              JOIN Orders AS o ON o.TownId = t.Id
              JOIN Clients AS c ON c.Id = o.ClientId
         GROUP BY t.Id, 
                  t.[Name])
     SELECT t.TownName,
            CASE
                WHEN t.Male >= 1
                THEN CAST(CAST(t.Male AS DECIMAL(15, 2)) / CAST(t.Total AS DECIMAL(15, 2)) * 100 AS INT)
                ELSE NULL
            END AS MalePercent,
            CASE
                WHEN t.Female >= 1
                THEN CAST(CAST(t.Female AS DECIMAL(15, 2)) / CAST(t.Total AS DECIMAL(15, 2)) * 100 AS INT)
                ELSE NULL
            END AS FemalePercent
     FROM t
     ORDER BY t.TownName, 
              t.Id