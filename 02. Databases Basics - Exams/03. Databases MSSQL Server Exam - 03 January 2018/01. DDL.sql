CREATE TABLE Clients
(Id           INT IDENTITY PRIMARY KEY, 
 FirstName    NVARCHAR(30) NOT NULL, 
 LastName     NVARCHAR(30) NOT NULL, 
 Gender       CHAR(1) NOT NULL
                      CHECK(Gender IN('M', 'F')), 
 BirthDate    DATETIME, 
 CreditCard   NVARCHAR(30) NOT NULL, 
 CardValidity DATETIME, 
 Email        NVARCHAR(50) NOT NULL
)

CREATE TABLE Towns
(Id     INT IDENTITY PRIMARY KEY, 
 [Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Offices
(Id            INT IDENTITY PRIMARY KEY, 
 [Name]        NVARCHAR(40), 
 ParkingPlaces INT, 
 TownId        INT NOT NULL, 
 CONSTRAINT FK_Offices_Towns FOREIGN KEY(TownId) REFERENCES Towns(Id)
)

CREATE TABLE Models
(Id             INT IDENTITY PRIMARY KEY, 
 Manufacturer   NVARCHAR(50) NOT NULL, 
 Model          NVARCHAR(50) NOT NULL, 
 ProductionYear DATETIME, 
 Seats          INT, 
 Class          NVARCHAR(10), 
 Consumption    DECIMAL(14, 2)
)

CREATE TABLE Vehicles
(Id       INT IDENTITY PRIMARY KEY, 
 ModelId  INT NOT NULL, 
 OfficeId INT NOT NULL, 
 Mileage  INT, 
 CONSTRAINT FK_Vehicles_Models FOREIGN KEY(ModelId) REFERENCES Models(Id), 
 CONSTRAINT FK_Vehicles_Offices FOREIGN KEY(OfficeId) REFERENCES Offices(Id)
)

CREATE TABLE Orders
(Id                 INT IDENTITY PRIMARY KEY, 
 ClientId           INT NOT NULL, 
 TownId             INT NOT NULL, 
 VehicleId          INT NOT NULL, 
 CollectionDate     DATETIME NOT NULL, 
 CollectionOfficeId INT NOT NULL, 
 ReturnDate         DATETIME, 
 ReturnOfficeId     INT, 
 Bill               DECIMAL(14, 2), 
 TotalMileage       INT, 
 CONSTRAINT FK_Orders_Clients FOREIGN KEY(ClientId) REFERENCES Clients(Id), 
 CONSTRAINT FK_Orders_Towns FOREIGN KEY(TownId) REFERENCES Towns(Id), 
 CONSTRAINT FK_Orders_Vehicles FOREIGN KEY(VehicleId) REFERENCES Vehicles(Id), 
 CONSTRAINT FK_Orders_Offices_CollectionOfficeId FOREIGN KEY(CollectionOfficeId) REFERENCES Offices(Id), 
 CONSTRAINT FK_Orders_Offices_ReturnOfficeId FOREIGN KEY(ReturnOfficeId) REFERENCES Offices(Id)
)