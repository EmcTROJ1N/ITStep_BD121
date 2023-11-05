-- Создание базы данных для телефонной книги
CREATE DATABASE PhoneBookDB;

GO
-- Использование созданной базы данных
USE PhoneBookDB;


CREATE TABLE Categories (
    id INT PRIMARY KEY IDENTITY(1, 1),
    category_name VARCHAR(255) NOT NULL,
    note VARCHAR(999)
);
INSERT INTO Categories (category_name, note) VALUES
('Family', 'Close family members'),
('Friends', 'Close friends and acquaintances'),
('Work', 'Colleagues and work-related contacts');


-- Create the Phones table
CREATE TABLE Phones (
    id INT PRIMARY KEY IDENTITY(1, 1),
    phone_number VARCHAR(15) NOT NULL,
    full_name VARCHAR(255) NOT NULL,
    note VARCHAR(999),
    creation_date DATE
);

-- Insert 10 records into the Phones table
INSERT INTO Phones (phone_number, full_name, note, creation_date) VALUES
('+71234567890', 'John Doe', 'Primary number', '2023-10-27'),
('+79876543210', 'Mary Smith', 'Home number', '2023-10-27'),
('+75551234567', 'Alex Johnson', 'Work number', '2023-10-27'),
('+73339997777', 'Elena Davis', 'Personal number', '2023-10-27'),
('+7775551111', 'Paul Taylor', 'Private number', '2023-10-27'),
('+78882224444', 'Olivia Brown', 'Alternative number', '2023-10-27'),
('+79996662222', 'Andrew Wilson', 'Relative', '2023-10-27'),
('+71113335555', 'Samantha Clark', 'Friend', '2023-10-27'),
('+72224446666', 'Denis Turner', 'Neighbor', '2023-10-27'),
('+74448889999', 'Natalie White', 'Acquaintance', '2023-10-27');


CREATE TABLE Contacts (
    id INT PRIMARY KEY IDENTITY(1, 1),
    FirstName VARCHAR(255) NOT NULL,
    LastName VARCHAR(255) NOT NULL,
    PhoneID INT NOT NULL,
    Email VARCHAR(255),
    Address VARCHAR(255),
    Birthday DATE,
    Notes VARCHAR(999),
    CategoryID INT,
    FOREIGN KEY (PhoneID) REFERENCES Phones(id),
    FOREIGN KEY (CategoryID) REFERENCES Categories(id)
);

INSERT INTO Contacts (FirstName, LastName, PhoneID, Email, Address, Birthday, Notes, CategoryID) VALUES
('John', 'Doe', 1, 'john@example.com', '123 Main St, Anytown, USA', '1990-02-20', 'High school friend', 1),
('Alice', 'Smith', 2, 'alice@example.com', '456 Elm St, Othertown, USA', '1987-07-15', 'Work colleague', 2),
('Mike', 'Johnson', 3, 'mike@example.com', '789 Oak St, Anothercity, USA', '1985-05-10', 'Neighbor', 3),
('Emily', 'Brown', 4, 'emily@example.com', '101 Pine St, Hometown, USA', '1992-11-30', 'Cousin', 1),
('David', 'Wilson', 5, 'david@example.com', '345 Cedar St, Newville, USA', '1988-03-05', 'Childhood friend', 2),
('Olivia', 'Miller', 6, 'olivia@example.com', '543 Birch St, Smalltown, USA', '1995-06-22', 'College roommate', 3),
('Michael', 'Taylor', 7, 'michael@example.com', '765 Maple St, Countryside, USA', '1984-09-12', 'Family friend', 1),
('Sarah', 'Anderson', 8, 'sarah@example.com', '987 Pine St, Mountainside, USA', '1981-12-18', 'Childhood buddy', 2),
('Daniel', 'Martin', 9, 'daniel@example.com', '234 Oak St, Riverside, USA', '1986-08-03', 'Co-worker', 3),
('Sophia', 'Moore', 10, 'sophia@example.com', '876 Elm St, Uptown, USA', '1989-01-25', 'Schoolmate', 1);

CREATE PROCEDURE GetContactList
AS
BEGIN
    SELECT c.id, c.FirstName, c.LastName,
           c.Email, c.Address, c.Birthday,
           c.Notes, p.phone_number, ca.category_name
    FROM Contacts c, Phones p, Categories ca
    WHERE c.PhoneID = p.id AND
          c.CategoryID = ca.id
END;