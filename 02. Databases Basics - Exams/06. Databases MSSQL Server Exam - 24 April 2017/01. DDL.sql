CREATE TABLE Clients
(ClientId  INT
 PRIMARY KEY IDENTITY, 
 FirstName VARCHAR(50), 
 LastName  VARCHAR(50), 
 Phone     CHAR(12)
)

CREATE TABLE Mechanics
(MechanicId INT
 PRIMARY KEY IDENTITY, 
 FirstName  VARCHAR(50), 
 LastName   VARCHAR(50), 
 [Address]  VARCHAR(255)
)

CREATE TABLE Models
(ModelId INT
 PRIMARY KEY IDENTITY, 
 [Name]  VARCHAR(50)
 UNIQUE
)

CREATE TABLE Jobs
(JobId      INT
 PRIMARY KEY IDENTITY, 
 ModelId    INT FOREIGN KEY REFERENCES Models(ModelId), 
 [Status]   VARCHAR(11) CHECK([Status] IN('Pending', 'In Progress', 'Finished'))
                        DEFAULT 'Pending', 
 ClientId   INT FOREIGN KEY REFERENCES Clients(ClientId), 
 MechanicId INT FOREIGN KEY REFERENCES Mechanics(MechanicId), 
 IssueDate  DATE, 
 FinishDate DATE
)

CREATE TABLE Orders
(OrderId   INT
 PRIMARY KEY IDENTITY, 
 JobId     INT FOREIGN KEY REFERENCES Jobs(JobId), 
 IssueDate DATE, 
 Delivered BIT DEFAULT 0
)

CREATE TABLE Vendors
(VendorId INT
 PRIMARY KEY IDENTITY, 
 [Name]   VARCHAR(50)
 UNIQUE NOT NULL
)

CREATE TABLE Parts
(PartId        INT
 PRIMARY KEY IDENTITY, 
 SerialNumber  VARCHAR(50)
 UNIQUE, 
 [Description] VARCHAR(255), 
 Price         DECIMAL(8, 2) CHECK(Price > 0), 
 VendorId      INT FOREIGN KEY REFERENCES Vendors(VendorId), 
 StockQty      INT CHECK(StockQty >= 0)
                   DEFAULT 0
)

CREATE TABLE OrderParts
(OrderId  INT FOREIGN KEY REFERENCES Orders(OrderId), 
 PartId   INT FOREIGN KEY REFERENCES Parts(PartId), 
 Quantity INT CHECK(Quantity > 0)
              DEFAULT 1, 
 CONSTRAINT PK_OrderParts PRIMARY KEY(OrderId, PartId)
)

CREATE TABLE PartsNeeded
(JobId    INT FOREIGN KEY REFERENCES Jobs(JobId), 
 PartId   INT FOREIGN KEY REFERENCES Parts(PartId), 
 Quantity INT CHECK(Quantity > 0)
              DEFAULT 1, 
 CONSTRAINT PK_PartsNeeded PRIMARY KEY(JobId, PartId)
)