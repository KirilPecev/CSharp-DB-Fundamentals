CREATE TABLE Categories(
   Id INT PRIMARY KEY IDENTITY,
   CategoryName NVARCHAR(50) NOT NULL,
   DailyRate INT NOT NULL,
   WeeklyRate INT NOT NULL,
   MonthlyRate INT NOT NULL,
   WeekendRate INT NOT NULL,
)

CREATE TABLE Cars(
   Id INT PRIMARY KEY IDENTITY,
   PlateNumber NVARCHAR(50) NOT NULL,
   Manufacturer NVARCHAR(50) NOT NULL,
   Model NVARCHAR(50) NOT NULL,
   CarYear DATETIME NOT NULL,
   CategoryId INT FOREIGN KEY REFERENCES Categories(Id)  NOT NULL,
   Doors int NOT NULL,
   Picture VARBINARY,
   Condition NVARCHAR,
   Available BIT DEFAULT 1
)

CREATE TABLE Employees(
   Id INT PRIMARY KEY IDENTITY,
   FirstName NVARCHAR(50) NOT NULL,
   LastName NVARCHAR(50) NOT NULL,
   Title NVARCHAR(50) NOT NULL,
   Notes NVARCHAR(MAX)
)

CREATE TABLE Customers(
   Id INT PRIMARY KEY IDENTITY,
   DriverLicenceNumber NVARCHAR(50) NOT NULL,
   FullName NVARCHAR(MAX) NOT NULL,
   Adress NVARCHAR(MAX) NOT NULL,
   City NVARCHAR(MAX) NOT NULL,
   ZIPCode NVARCHAR(5) NOT NULL,
   Notes NVARCHAR(MAX)
)

CREATE TABLE RentalOrders(
   Id INT PRIMARY KEY IDENTITY,
   EmployeeId INT FOREIGN KEY REFERENCES Employees(Id) NOT NULL,
   CustomerId INT FOREIGN KEY REFERENCES Customers(Id) NOT NULL,
   CarId INT FOREIGN KEY REFERENCES Cars(Id)  NOT NULL,
   TankLevel INT NOT NULL,
   KilometrageStart INT NOT NULL,
   KilometrageEnd INT NOT NULL,
   TotalKilometrage INT NOT NULL,
   StartDate DATETIME2 NOT NULL,
   EndDate DATETIME2 NOT NULL,
   TotalDays INT NOT NULL,
   RateApplied INT NOT NULL,
   TaxRate INT NOT NULL,
   OrderStatus NVARCHAR(20) NOT NULL,
   NOTES NVARCHAR(MAX)
)

INSERT INTO Categories (CategoryName,DailyRate,WeeklyRate,MonthlyRate,WeekendRate)
VALUES
('Category 1', 10, 70, 310, 20),
('Category 2', 11, 71, 311, 21),
('Category 3', 12, 72, 312, 22)

INSERT INTO Cars(PlateNumber, Manufacturer, Model, CarYear, CategoryId, Doors, Picture, Condition, Available)
VALUES
('E9465KA', 'VW', 'Passat', CONVERT(DATETIME,'06.11.1999',104), 3, 4, 11111111111, NULL,1),
('E1561BC', 'VW', 'Golf', CONVERT(DATETIME,'06.11.1998',104), 2, 4, 101010101, NULL,1),
('CB1998KA', 'BMW', 'M5', CONVERT(DATETIME,'06.11.2018',104), 1, 4, 1010101010, NULL,1)

INSERT INTO Employees(FirstName, LastName, Title, Notes)
VALUES
('Pesho', 'Peshov', 'AbraKadaBra 1', NUll),
('Gosho', 'GOshov', 'AbraKadaBra 2', NUll),
('Kiro', 'Kirov', 'AbraKadaBra 3', NUll)


INSERT INTO Customers(DriverLicenceNumber,FullName,Adress,City,ZIPCode,Notes)
VALUES
('CA1918EE', 'Ivan Ivanov Ivanovski', 'Ivan Vazov 10', 'Sofia', '1000', NULL),
('CA1212FD', 'Gosho Ivanov Ivanovski', 'Ivan Vazov 12', 'Sofia', '1000', NULL),
('CA2545CG', 'Kiro Ivanov Ivanchevski', 'Ivan Vazov 16', 'Sofia', '1000', NULL)

INSERT INTO RentalOrders(EmployeeId, CustomerId, CarId, TankLevel, KilometrageStart, KilometrageEnd, 
                          TotalKilometrage, StartDate, EndDate, TotalDays, RateApplied, TaxRate, OrderStatus, NOTES)
VALUES
(1,2,3,65,100000,200000,300000,CONVERT(DATETIME,'06.11.1999',104),CONVERT(DATETIME,'06.11.1999',104),12548,1221313,12312,'OrderStatus 1', NULL),
(2,1,1,60,100000,200000,300000,CONVERT(DATETIME,'06.11.2000',104),CONVERT(DATETIME,'06.11.1999',104),12548,1221313,12312,'OrderStatus 2', NULL),
(3,3,2,55,100000,200000,300000,CONVERT(DATETIME,'06.11.2005',104),CONVERT(DATETIME,'06.11.1999',104),12548,1221313,12312,'OrderStatus 3', NULL)
