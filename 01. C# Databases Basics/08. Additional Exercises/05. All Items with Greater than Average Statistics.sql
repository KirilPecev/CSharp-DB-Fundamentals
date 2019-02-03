WITH AVGStats_CTE(AVGMind, 
                  AVGLuck, 
                  AVGSpeed)
     AS (SELECT AVG(Mind) AS AVGMind, 
                AVG(Luck) AS AVGLuck, 
                AVG(Speed) AS AVGSpeed
         FROM [Statistics])

     SELECT i.[Name], 
            i.Price, 
            i.MinLevel, 
            s.Strength, 
            s.Defence, 
            s.Speed, 
            s.Luck, 
            s.Mind
     FROM Items AS i
          JOIN [Statistics] AS s ON s.Id = i.StatisticId
     WHERE s.Mind >
     (
         SELECT AVGMind
         FROM AVGStats_CTE
     )
	  AND s.Luck >
     (
         SELECT AVGLuck
         FROM AVGStats_CTE
     )
	 AND s.Speed >
     (
         SELECT AVGSpeed
         FROM AVGStats_CTE
     )
     ORDER BY i.[Name]