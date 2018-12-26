CREATE TABLE People(
   Id INT IDENTITY NOT NULL,
   [Name] NVARCHAR(50) NOT NULL,
   Birthdate DATETIME2 NOT NULL
   
   CONSTRAINT PK_People_Id
   PRIMARY KEY(Id) 
)

INSERT INTO People (Name, Birthdate)
VALUES
('Victor', CONVERT(DATETIME,'2000-12-07 00:00:00.000', 121)),
('Steven', CONVERT(DATETIME,'1992-09-10 00:00:00.000', 121)),
('Stephen', CONVERT(DATETIME,'1910-09-19 00:00:00.000', 121)),
('John', CONVERT(DATETIME,'2010-01-06 00:00:00.000', 121))

SELECT Name, DATEDIFF(YEAR, Birthdate, GETDATE()) AS [Age in Years], 
             DATEDIFF(MONTH, Birthdate, GETDATE()) AS [Age in Months],
             DATEDIFF(DAY, Birthdate, GETDATE()) AS [Age in Days], 
             DATEDIFF(MINUTE, Birthdate, GETDATE()) AS [Age in Minutes]
FROM People