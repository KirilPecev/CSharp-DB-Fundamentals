SELECT TownID, Name
FROM Towns
WHERE NOT (LEFT(Name, 1) = 'R'
      OR LEFT(Name, 1) = 'B'
      OR  LEFT(Name, 1) = 'D')
ORDER BY Name