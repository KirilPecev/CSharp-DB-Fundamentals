SELECT FirstName, 
       [Address], 
       Phone
FROM Students
WHERE MiddleName IS NOT NULL
      AND Phone LIKE '42%'
ORDER BY FirstName