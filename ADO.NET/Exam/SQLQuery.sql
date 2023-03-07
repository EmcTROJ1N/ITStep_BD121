CREATE DATABASE PlanesManufactures;

GO

USE PlanesManufactures;

CREATE TABLE Manufacturers (
  ManufacturerId INT NOT NULL PRIMARY KEY,
  BrandTitle VARCHAR(255),
  Address VARCHAR(255),
  Phone VARCHAR(50)
);

CREATE TABLE Planes (
  Id INT NOT NULL PRIMARY KEY,
  Model VARCHAR(255),
  Price DECIMAL(10,2),
  Speed INT,
  ManufacturerId INT
);

INSERT INTO Manufacturers (ManufacturerId, BrandTitle, Address, Phone) 
VALUES (1, 'Boeing', 'Chicago, IL', '+1 (312) 544-2000');

INSERT INTO Manufacturers (ManufacturerId, BrandTitle, Address, Phone) 
VALUES (2, 'Airbus', 'Toulouse, France', '+33 (0)5 61 93 33 33');

INSERT INTO Planes (Id, Model, Price, Speed, ManufacturerId) 
VALUES (1, 'Boeing 737', 80000000, 583, 1);

INSERT INTO Planes (Id, Model, Price, Speed, ManufacturerId) 
VALUES (2, 'Boeing 747', 3900000, 570, 1);

INSERT INTO Planes (Id, Model, Price, Speed, ManufacturerId) 
VALUES (3, 'Airbus A320', 1010000, 517, 2);

INSERT INTO Planes (Id, Model, Price, Speed, ManufacturerId) 
VALUES (4, 'Airbus A380', 4456000, 1020, 2);

CREATE PROCEDURE GetAllPlanesByManufacturer(@ManufacturerName VARCHAR(255))
AS
BEGIN
  SELECT Id, Model, Price, Address, Phone FROM Planes, Manufacturers
  where Planes.ManufacturerId in
  (select ManufacturerId from Manufacturers
  where BrandTitle = @ManufacturerName)
END

select * from Planes
select * from Manufacturers


CREATE DATABASE Company;

GO

USE Company;

CREATE TABLE Departments (
  DepartmentId INT PRIMARY KEY,
  Title VARCHAR(255),
  HeadId INT,
  Address VARCHAR(255),
  PhoneNumber VARCHAR(255)
);

CREATE TABLE Employees (
  Employee_id INT PRIMARY KEY,
  FirstName VARCHAR(255),
  LastName VARCHAR(255),
  Age INT,
  Address VARCHAR(255),
  PhotoPath VARCHAR(255)
);

CREATE TABLE DepartmentsEmployees (
  DepartmentId INT,
  EmployeeId INT,
  PRIMARY KEY (DepartmentId, EmployeeId)
);

INSERT INTO Departments (DepartmentId, Title, HeadId, Address, PhoneNumber) VALUES 
(1, 'Engineering', 1001, '123 Main St', '555-1234'),
(2, 'Sales', 1002, '456 Market St', '555-5678'),
(3, 'Marketing', 1003, '789 Broadway', '555-9012');

INSERT INTO Employees (Employee_id, FirstName, LastName, Age, Address, PhotoPath) VALUES
(1001, 'John', 'Doe', 30, '123 Main St', 'john.jpg'),
(1002, 'Jane', 'Smith', 25, '456 Market St', 'jane.jpg'),
(1003, 'Bob', 'Johnson', 35, '789 Broadway', 'bob.jpg');

INSERT INTO DepartmentsEmployees (DepartmentId, EmployeeId) VALUES
(1, 1001),
(1, 1002),
(2, 1003);

CREATE PROCEDURE GetAllEmployeesFromDepartment(@DepartmentID INT)
AS
BEGIN
  SELECT * FROM Employees
  where Employee_id in
  (select Employee_id from DepartmentsEmployees
  where DepartmentId = @DepartmentID)
END

select * from Departments
select * from DepartmentsEmployees
select * from Employees

EXEC GetAllPlanesByManufacturer 'Boeing'

update Employees
set PhotoPath = 'C:\Users\19et7\Desktop\ADO.NET\Exam\Imgs\Jhon.jpg'
where Employee_id = 1001