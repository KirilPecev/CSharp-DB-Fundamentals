CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns (
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL,
)

CREATE TABLE Addresses (
	Id INT PRIMARY KEY IDENTITY, 
	AddressText NVARCHAR(MAX) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns (Id) NOT NULL
)

CREATE TABLE Departments (
	Id INT PRIMARY KEY IDENTITY, 
	NAME NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees (
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(200) NOT NULL,
	MiddleName NVARCHAR(200),
	LastName NVARCHAR(200) NOT NULL,
	JobTitle NVARCHAR(200) NOT NULL, 
	DepartmentId INT FOREIGN KEY REFERENCES Departments (Id) NOT NULL, 
	HireDate DATE NOT NULL, 
	Salary DECIMAL(10,2) CHECK (Salary >= 0) NOT NULL, 
	AddressId INT FOREIGN KEY REFERENCES Addresses (Id),
)