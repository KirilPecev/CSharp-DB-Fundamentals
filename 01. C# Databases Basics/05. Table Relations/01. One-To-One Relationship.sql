CREATE TABLE Passports(
	PassportID INT NOT NULL IDENTITY(101,1),
	PassportNumber NVARCHAR(8) NOT NULL

	CONSTRAINT PK_Passports
	PRIMARY KEY(PassportID)
)

INSERT INTO Passports(PassportNumber)
VALUES
('N34FG21B'),
('K65LO4R7'),
('ZE657QP2')

CREATE TABLE Persons(
	PersonID INT NOT NULL IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	Salary DECIMAL(15,2) NOT NULL,
	PassportID INT NOT NULL

	CONSTRAINT PK_Persons
	PRIMARY KEY(PersonID),
	CONSTRAINT FK_Persons_Passports
	FOREIGN KEY(PassportID)
	REFERENCES Passports
)

INSERT INTO Persons(FirstName, Salary, PassportID)
VALUES
('Roberto', 43300.00, 102),
('Tom', 56100.00, 103),
('Yana', 60200.00, 101)
