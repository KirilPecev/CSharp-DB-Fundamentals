CREATE DATABASE Movies
GO

USE Movies

CREATE TABLE Directors(
Id INT PRIMARY KEY,
DirectorName NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Genres(
Id INT PRIMARY KEY,
GenreName NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Categories(
Id INT PRIMARY KEY,
CategoryName NVARCHAR(50) NOT NULL,
Notes NVARCHAR(MAX)
)

CREATE TABLE Movies(
Id INT PRIMARY KEY IDENTITY,
Title NVARCHAR(50) NOT NULL,
DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
CopyrightYear DATETIME NOT NULL,
[Length] INT NOT NULL,
GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
Rating INT NOT NULL,
Notes NVARCHAR(MAX)
)

INSERT INTO Directors(Id,DirectorName,Notes) 
VALUES
(1, 'Pesho', 'asdas'),
(2, 'Gosho', 'sdasdasd'),
(3,'Misho', 'asasdasddas'),
(4,'Roman', 'asasdasddas'),
(5,'Stephan', 'asasdasddas')

INSERT INTO Genres(Id,GenreName,Notes) 
VALUES
(1,'Action', 'asdas'),
(2,'Adventure', 'sdasdasd'),
(3,'Comedy', 'asasdasddas'),
(4,'Crime', 'asasdasddas'),
(5,'Drama', 'asasdasddas')

INSERT INTO Categories(Id,CategoryName,Notes)
VALUES
(1,'Action', 'asasdasddas'),
(2,'Adventure', 'asasdasddas'),
(3,'Comedy', 'asasdasddas'),
(4,'Crime', 'asasdasddas'),
(5,'Drama', 'asasdasddas')

INSERT INTO Movies(Title,DirectorId,CopyrightYear,Length,GenreId,CategoryId,Rating,Notes)
Values
('asdSa',1,CONVERT(datetime, '19.05.1998',104),50,1,1,10,'ASDSAD'),
('12312',2,CONVERT(datetime, '20.02.1998',104),10,2,2,9,'ASASDDSAD'),
('aqweq',3,CONVERT(datetime, '05.03.1998',104),15,3,3,8,'AASDSDSAD'),
('tyuty',4,CONVERT(datetime, '12.05.1998',104),25,4,4,7,'ASDASDSAD'),
('aghja',5,CONVERT(datetime, '16.01.1998',104),35,5,5,3,'ASASDDSAD')