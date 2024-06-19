CREATE DATABASE Warehouse;
GO

USE Warehouse;
GO

CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY,
    Name NVARCHAR(100) NOT NULL,
    TypeId INT NOT NULL,
    SupplierId INT NOT NULL,
    Quantity INT NOT NULL
);

CREATE TABLE ProductTypes (
    Id INT PRIMARY KEY IDENTITY,
    TypeName NVARCHAR(100) NOT NULL
);

CREATE TABLE Suppliers (
    Id INT PRIMARY KEY IDENTITY,
    SupplierName NVARCHAR(100) NOT NULL
);

INSERT INTO ProductTypes (TypeName) VALUES ('Electronics'), ('Furniture'), ('Clothing');
INSERT INTO Suppliers (SupplierName) VALUES ('Supplier A'), ('Supplier B'), ('Supplier C');
INSERT INTO Products (Name, TypeId, SupplierId, Quantity) VALUES 
('TV', 1, 1, 50),
('Sofa', 2, 2, 20),
('T-Shirt', 3, 3, 100);