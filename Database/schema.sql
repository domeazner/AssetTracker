
-- ==================================================================================================
-- ScottishGlen Asset Tracking System: database schema (Azure SQL). This is ran first, then seed.sql.

-- Three tables, linked by foreign keys: Department (1) ---< Employee (1) ---< Asset
-- One department has many employees & one employee can have many assets.
-- Tables are created parent first so each foreign key points at a table that already exists.
-- ==================================================================================================

-- Lookup table for the departments in the company

CREATE TABLE Department (
    
    -- IDENTITY(1,1): the database numbers rows sort automatically & starting at 1.

    DepartmentId   INT IDENTITY(1,1) PRIMARY KEY,

    -- UNIQUE stops the same company department being entered twice.

    DepartmentName NVARCHAR(50) NOT NULL UNIQUE
);


-- These are the departments from the requirements, they are inserted here because they will not change, the app needs no screen management.
-- IDs are assigned numerically 1 = Finance, 2 = Human Resources, 3 = Operations, 4 = Sales, 5 = Information Technology.

INSERT INTO Department (DepartmentName) VALUES
 ('Finance'), ('Human Resources'), ('Operations'), ('Sales'), ('Information Technology');

-- Individuals who are assigned assets.

CREATE TABLE Employee (
    EmployeeId   INT IDENTITY(1,1) PRIMARY KEY,
    FirstName    NVARCHAR(50)  NOT NULL,
    LastName     NVARCHAR(50)  NOT NULL,
    EmailAddress NVARCHAR(100) NOT NULL UNIQUE,
    DepartmentId INT NOT NULL REFERENCES Department(DepartmentId)
);

-- Main table, one row per tracked device.

CREATE TABLE Asset (

    -- UNIQUEIDENTIFIER is SQL Server's GUID (Globally Unique Identifier) type. A GUID is used instead of an incrementing number so IDs are unique and hard to guess. The app generates the Guid in C#; DEFAULT NEWID() is a safety net that fills it in if an insert should ever arrives without one.

    AssetId       UNIQUEIDENTIFIER PRIMARY KEY DEFAULT NEWID(),
    
    -- Harware details via WMI (Windows Management Instrumentation) are collected automatically from the machine

    SystemName    NVARCHAR(100) NOT NULL,
    Model         NVARCHAR(100) NOT NULL,
    Manufacturer  NVARCHAR(100) NOT NULL,
    AssetType     NVARCHAR(50)  NOT NULL,

    -- The longest possible IPv6 addresss is 45 characters, therefore both IPv4 and v6 can fit.

    IpAddress     NVARCHAR(45)  NOT NULL,
    
    -- These are optional fields, NULL is allowed, matching the optional form inputs.
    
    PurchaseDate  DATE NULL,
    Notes         NVARCHAR(MAX) NULL,
    
    -- Foreign Key: 'an asset must be assigned to a real employee, which prevents orphan records. It is not null due to the fact that every asset needs an owner.
    
    EmployeeId    INT NOT NULL REFERENCES Employee(EmployeeId)
);
