SELECT FirstName + ' ' + isnull(MiddleName,'') + ' ' + LastName AS [Full Name], 
       [Address]
FROM Students
WHERE [Address] LIKE '%road%'
ORDER BY FirstName, 
         LastName, 
         [Address]
