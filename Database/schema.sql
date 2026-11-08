CREATE TABLE Department (
    DepartmentId   INT IDENTITY(1,1) PRIMARY KEY,
    DepartmentName NVARCHAR(50) NOT NULL UNIQUE
);

INSERT INTO Department (DepartmentName) VALUES
 ('Finance'), ('Human Resources'), ('Operations'), ('Sales'), ('Information Technology');

CREATE TABLE Employee (
    EmployeeId   INT IDENTITY(1,1) PRIMARY KEY,
    FirstName    NVARCHAR(50)  NOT NULL,
    LastName     NVARCHAR(50)  NOT NULL,
    EmailAddress NVARCHAR(100) NOT NULL UNIQUE,
    DepartmentId INT NOT NULL REFERENCES Department(DepartmentId)
);

CREATE TABLE Asset (
    AssetId       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    SystemName    NVARCHAR(100) NOT NULL,
    Model         NVARCHAR(100) NOT NULL,
    Manufacturer  NVARCHAR(100) NOT NULL,
    AssetType     NVARCHAR(50)  NOT NULL,
    IpAddress     NVARCHAR(45)  NOT NULL,
    PurchaseDate  DATE NULL,
    Notes         NVARCHAR(MAX) NULL,
    EmployeeId    INT NOT NULL REFERENCES Employee(EmployeeId)
);
