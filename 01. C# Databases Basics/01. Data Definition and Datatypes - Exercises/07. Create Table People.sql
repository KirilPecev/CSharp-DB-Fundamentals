CREATE TABLE People(
  Id INT PRIMARY KEY IDENTITY,
  [Name] NVARCHAR(200) NOT NULL,
  Picture VARBINARY(2048),
  Height FLOAT(2),
  [Weight] FLOAT(2),
  Gender char(1) NOT NUll,
  Birthdate DATETIME2 NOT NULL,
  Biography NVARCHAR(MAX)
)

INSERT INTO People(Name, Picture, Height, Weight, Gender, Birthdate, Biography) 
VALUES
('Pesho', 1111111, 1.80, 80, 'm', CONVERT(DATETIME,'15.05.2000',104), 'Lieutenant'),
('Gosho', 000000, 1.60, 75, 'm', CONVERT(DATETIME,'20.01.2003',104), 'Footballer'),
('Maria', 1110101111, 1.75, 69, 'f', CONVERT(DATETIME,'01.06.2005',104), 'Dancer'),
('Laura', 11110111, 1.70, 60, 'f', CONVERT(DATETIME,'02.03.1990',104), 'Dancer'),
('Stephan', 111110111, 1.80, 81.5, 'm', CONVERT(DATETIME,'11.09.2001',104), 'Driver')