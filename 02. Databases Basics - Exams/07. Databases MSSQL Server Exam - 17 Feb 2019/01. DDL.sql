CREATE DATABASE School
GO
USE School
GO

CREATE TABLE Students
(Id         INT
 PRIMARY KEY IDENTITY, 
 FirstName  NVARCHAR(30) NOT NULL, 
 MiddleName NVARCHAR(25), 
 LastName   NVARCHAR(30) NOT NULL, 
 Age        INT CHECK(Age > 0), 
 [Address]  NVARCHAR(50), 
 Phone      NCHAR(10)
)

CREATE TABLE Subjects
(Id      INT
 PRIMARY KEY IDENTITY, 
 [Name]  NVARCHAR(20) NOT NULL, 
 Lessons INT CHECK(Lessons > 0) NOT NULL
)

CREATE TABLE StudentsSubjects
(Id        INT
 PRIMARY KEY IDENTITY, 
 StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL, 
 SubjectId INT FOREIGN KEY REFERENCES Subjects(Id) NOT NULL, 
 Grade     DECIMAL(15, 2) CHECK(Grade >= 2
                               AND Grade <= 6) NOT NULL
)

CREATE TABLE Exams
(Id        INT
 PRIMARY KEY IDENTITY, 
 [Date]    DATETIME, 
 SubjectId INT FOREIGN KEY REFERENCES Subjects(Id) NOT NULL,
)

CREATE TABLE StudentsExams
(StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL, 
 ExamId    INT FOREIGN KEY REFERENCES Exams(Id) NOT NULL, 
 Grade     DECIMAL(15, 2) CHECK(Grade >= 2
                               AND Grade <= 6) NOT NULL, 
 PRIMARY KEY(StudentId, ExamId)
)

CREATE TABLE Teachers
(Id        INT
 PRIMARY KEY IDENTITY, 
 FirstName NVARCHAR(20) NOT NULL, 
 LastName  NVARCHAR(20) NOT NULL, 
 [Address] NVARCHAR(20) NOT NULL, 
 Phone     CHAR(10), 
 SubjectId INT FOREIGN KEY REFERENCES Subjects(Id)
)

CREATE TABLE StudentsTeachers
(StudentId INT FOREIGN KEY REFERENCES Students(Id) NOT NULL, 
 TeacherId INT FOREIGN KEY REFERENCES Teachers(Id) NOT NULL, 
 PRIMARY KEY(StudentId, TeacherId)
)