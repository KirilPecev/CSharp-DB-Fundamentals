CREATE DATABASE Supermarket
GO

USE Supermarket
GO

CREATE TABLE Categories(
	Id INT IDENTITY UNIQUE,
	[Name] NVARCHAR(30) NOT NULL

	CONSTRAINT PK_Categories
	PRIMARY KEY(Id)
)

CREATE TABLE Items(
	Id INT IDENTITY UNIQUE,
	[Name] NVARCHAR(30) NOT NULL,
	Price DECIMAL(18,2) NOT NULL,
	CategoryId INT NOT NULL,

	CONSTRAINT PK_Items
	PRIMARY KEY(Id),
	CONSTRAINT FK_Items_Categories
	FOREIGN KEY(CategoryId) REFERENCES Categories(Id)
)

CREATE TABLE Employees(
	Id INT IDENTITY UNIQUE,
	FirstName NVARCHAR(50) NOT NULL,
	LastName NVARCHAR(50) NOT NULL,
	Phone CHAR(12) NOT NULL,
	Salary DECIMAL(18,2) NOT NULL,

	CONSTRAINT PK_Employees
	PRIMARY KEY(Id),
)

CREATE TABLE Orders(
	Id INT IDENTITY UNIQUE,
	[DateTime] DATETIME NOT NULL,
	EmployeeId INT NOT NULL,

	CONSTRAINT PK_Orders
	PRIMARY KEY(Id),
	CONSTRAINT FK_Orders_Employees
	FOREIGN KEY(EmployeeId) REFERENCES Employees(Id)
)

CREATE TABLE OrderItems(
	OrderId INT NOT NULL,
	ItemId INT NOT NULL,
	Quantity INT NOT NULL,

	CONSTRAINT PK_OrderItems
	PRIMARY KEY(OrderId,ItemId),

	CONSTRAINT FK_OrdersItems_Orders
	FOREIGN KEY(OrderId) REFERENCES Orders(Id),

	CONSTRAINT FK_OrdersItems_Items
	FOREIGN KEY(ItemId) REFERENCES Items(Id),

	CONSTRAINT CHK_Quantity CHECK (Quantity >= 1)
)

CREATE TABLE Shifts(
	Id INT IDENTITY,
	EmployeeId INT,
	CheckIn DATETIME NOT NULL,
	CheckOut DATETIME NOT NULL,

	CONSTRAINT PK_Shifts 
	PRIMARY KEY (Id, EmployeeId),

	CONSTRAINT FK_Shifts_Employees
	FOREIGN KEY(EmployeeId) REFERENCES Employees(Id),

	CONSTRAINT CHK_CheckOut CHECK (CheckOut > CheckIn )
)