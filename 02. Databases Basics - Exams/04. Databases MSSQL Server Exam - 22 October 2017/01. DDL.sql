CREATE TABLE Users
(Id         INT
 PRIMARY KEY IDENTITY, 
 Username   NVARCHAR(30)
 UNIQUE NOT NULL, 
 [Password] NVARCHAR(50) NOT NULL, 
 [Name]     NVARCHAR(50), 
 Gender     CHAR(1) CHECK(Gender = 'M'
                          OR Gender = 'F'), 
 BirthDate  DATETIME, 
 Age        INT, 
 Email      NVARCHAR(50) NOT NULL
)

CREATE TABLE Departments
(Id     INT
 PRIMARY KEY IDENTITY, 
 [Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees
(Id           INT
 PRIMARY KEY IDENTITY, 
 FirstName    NVARCHAR(25), 
 LastName     NVARCHAR(25), 
 Gender       CHAR(1) CHECK(Gender = 'M'
                            OR Gender = 'F'), 
 BirthDate    DATETIME, 
 Age          INT, 
 DepartmentId INT FOREIGN KEY REFERENCES Departments(Id) NOT NULL
)

CREATE TABLE Categories
(Id           INT
 PRIMARY KEY IDENTITY, 
 [Name]       VARCHAR(50) NOT NULL, 
 DepartmentId INT FOREIGN KEY REFERENCES Departments(Id)
)

CREATE TABLE [Status]
(Id    INT
 PRIMARY KEY IDENTITY, 
 Label VARCHAR(30) NOT NULL
)

CREATE TABLE Reports
(Id            INT
 PRIMARY KEY IDENTITY, 
 CategoryId    INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL, 
 StatusId      INT FOREIGN KEY REFERENCES [Status](Id) NOT NULL, 
 OpenDate      DATETIME NOT NULL, 
 CloseDate     DATETIME, 
 [Description] VARCHAR(200), 
 UserId        INT FOREIGN KEY REFERENCES Users(Id) NOT NULL, 
 EmployeeId    INT FOREIGN KEY REFERENCES Employees(Id)
)