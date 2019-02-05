  SELECT CONCAT_WS(' ',FirstName,LastName) AS [Full Name],
  	     Phone AS [Phone Number]
    FROM Employees
   WHERE Phone LIKE '3%'
ORDER BY FirstName, Phone