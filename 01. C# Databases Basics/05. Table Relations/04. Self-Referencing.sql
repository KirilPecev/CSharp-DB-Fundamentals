CREATE TABLE Teachers(
	TeacherID INT NOT NULL IDENTITY(101,1),
	Name NVARCHAR(50) NOT NULL,
	ManagerID INT,

	CONSTRAINT PK_Teachers
	PRIMARY KEY(TeacherID),

	CONSTRAINT FK_Teachers_Teachers
	FOREIGN KEY(ManagerID)
	REFERENCES Teachers 
)

INSERT INTO Teachers(Name, ManagerID)
VALUES
('John', Null),
('Maya', 106),
('Silvia', 106),
('Teb', 105),
('Mark', 101),
('Greta', 101)
		