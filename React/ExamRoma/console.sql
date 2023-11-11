create database ContactsDB

GO
-- Использование созданной базы данных
USE ContactsDB;

CREATE TABLE Categories (
    id INT PRIMARY KEY IDENTITY(1, 1),
    category_name VARCHAR(255) NOT NULL,
);
INSERT INTO Categories (category_name) VALUES
('Family'),
('Friends'),
('Work');

-- Create the Phones table
CREATE TABLE Phones (
    id INT PRIMARY KEY IDENTITY(1, 1),
    phone_number VARCHAR(15) NOT NULL
);

INSERT INTO Phones (phone_number) VALUES
('+71234567890'),
('+79876543210'),
('+75551234567'),
('+73339997777'),
('+7775551111'),
('+78882224444'),
('+79996662222'),
('+71113335555'),
('+72224446666'),
('+74448889999');


CREATE TABLE Contacts (
    id INT PRIMARY KEY IDENTITY(1, 1),
    FullName VARCHAR(255) NOT NULL,
    PhoneID INT NOT NULL,
    CategoryID INT,
);

INSERT INTO Contacts (FullName, PhoneID, CategoryID) VALUES
('John Doe', 1, 1),
('Alice Smith', 2, 2),
('Mike Johnson', 3, 3),
('Emily Brown', 4, 1),
('David Wilson', 5, 2),
('Olivia Miller', 6, 3),
('Michael Taylor', 7, 1),
('Sarah Anderson', 8, 2),
('Daniel Martin', 9, 3),
('Sophia Moore', 10, 1);