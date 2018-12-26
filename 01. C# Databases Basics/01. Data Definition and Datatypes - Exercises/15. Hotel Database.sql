CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(200) NOT NULL, 
	LastName NVARCHAR(200) NOT NULL, 
	Title NVARCHAR(200) NOT NULL, 
	Notes NVARCHAR(MAX)
)

CREATE TABLE Customers (
	AccountNumber INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(200) NOT NULL, 
	LastName NVARCHAR(200) NOT NULL, 
	PhoneNumber bigint NOT NULL CHECK (PhoneNumber>0),
	EmergencyName NVARCHAR(200),
	EmergencyNumber bigint,
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomStatus (
	RoomStatus NVARCHAR(200) PRIMARY KEY NOT NULL, 
	Notes NVARCHAR(MAX)
)

CREATE TABLE RoomTypes (
	RoomType NVARCHAR(200) PRIMARY KEY NOT NULL, 
	Notes NVARCHAR(MAX)
)

CREATE TABLE BedTypes (
	BedType NVARCHAR(200)  PRIMARY KEY  NOT NULL, 
	Notes NVARCHAR(MAX)
)

CREATE TABLE Rooms (
	RoomNumber INT PRIMARY KEY IDENTITY,
	RoomType NVARCHAR(200)FOREIGN KEY REFERENCES RoomTypes (RoomType) NOT NULL,
	BedType NVARCHAR(200) NOT NULL,
	Rate DECIMAL(5,2) NOT NULL,
	RoomStatus NVARCHAR(200) NOT NULL,
	Note NVARCHAR(MAX),
	FOREIGN KEY (BedType) REFERENCES BedTypes (BedType),
	FOREIGN KEY (RoomStatus) REFERENCES RoomStatus (RoomStatus),
	CHECK (Rate >= 0)	
)

CREATE TABLE Payments (
	Id INT IDENTITY, 
	EmployeeId INT NOT NULL, 
	PaymentDate DATE NOT NULL, 
	AccountNumber INT NOT NULL, 
	FirstDateOccupied DATE NOT NULL, 
	LastDateOccupied DATE NOT NULL, 
	TotalDays INT NOT NULL, 
	AmountCharged DECIMAL(5,2) NOT NULL, 
	TaxRate DECIMAL(5,2) NOT NULL, 
	TaxAmount DECIMAL(5,2) NOT NULL, 
	PaymentTotal DECIMAL(5,2) NOT NULL, 
	Notes NVARCHAR(MAX),
	PRIMARY KEY (Id),
	FOREIGN KEY (EmployeeId) REFERENCES Employees (Id),
	FOREIGN KEY (AccountNumber) REFERENCES Customers (AccountNumber),
	CHECK (TotalDays = DATEDIFF(DAY, FirstDateOccupied, LastDateOccupied)),
)

CREATE TABLE Occupancies (
	Id INT IDENTITY, 
	EmployeeId INT NOT NULL, 
	DateOccupied DATE NOT NULL, 
	AccountNumber INT NOT NULL, 
	RoomNumber INT NOT NULL, 
	RateApplied DECIMAL(5,2) NOT NULL, 
	PhoneCharge DECIMAL(5,2) NOT NULL DEFAULT 0, 
	Notes NVARCHAR(MAX),
	PRIMARY KEY (Id),
	FOREIGN KEY (EmployeeId) REFERENCES Employees (Id),
	FOREIGN KEY (AccountNumber) REFERENCES Customers (AccountNumber),
	FOREIGN KEY (RoomNumber) REFERENCES Rooms (RoomNumber),
	CHECK (PhoneCharge >= 0)
)

INSERT INTO Employees (FirstName, LastName, Title, Notes)
VALUES
	('Tom', 'Barnes', 'Hotel Manager', NULL),
	('David', 'Jones', 'CEO', NULL),
	('Eva', 'Michado', 'Chambermaid', 'Late for work')
	
INSERT INTO Customers (FirstName, LastName, PhoneNumber, EmergencyName, EmergencyNumber)
VALUES
	('Angela','Merkel', 49123456789, 'Barroso', 32987654321),
	('Barack','Obama', 1123456789, NULL, NULL),
	('Margaret','Thacher', 41987654321, NULL, NULL)

INSERT INTO RoomStatus (RoomStatus)
VALUES
	('Reserved'), ('Occupied'), ('Available')

INSERT INTO RoomTypes (RoomType)
VALUES
	('Single'), ('Double'), ('Suite')

INSERT INTO BedTypes (BedType)
VALUES
	('Single'), ('Twin'), ('Double')

INSERT INTO Rooms (RoomType, BedType, Rate, RoomStatus)
VALUES
	('Single', 'Single', 70, 'Reserved'),
	('Double', 'Twin', 100, 'Occupied'),
	('Suite', 'Double', 110, 'Available')

INSERT INTO Payments (EmployeeId, PaymentDate, AccountNumber, FirstDateOccupied, LastDateOccupied, TotalDays, AmountCharged, TaxRate, TaxAmount, PaymentTotal)
VALUES
	(1, '2017-01-22', 1, '2017-01-21', '2017-01-22', 1, 100, 0.20, 20, 120),
	(1, '2017-01-22', 2, '2017-01-20', '2017-01-22', 2, 200, 0.20, 40, 240),
	(1, '2017-01-22', 3, '2017-01-19', '2017-01-22', 3, 300, 0.20, 60, 360)

INSERT INTO Occupancies (EmployeeId, DateOccupied, AccountNumber, RoomNumber, RateApplied, PhoneCharge)
VALUES
	(1, '2014-01-22', 1, 1, 70, 0),
	(2, '2014-01-22', 2, 2, 100, 0),
	(3, '2014-01-22', 3, 3, 110, 10)