
-- ============================================================================================================
-- Below is sample data so the Add Asset form has employees to choose from. Run after schema.sql. The email addresses use the ".example" domain, this is a placeholder that will be changed in the part 3 or if we are given dummy data to be included.

-- DepartmentId values depend on the order of the Department inserts in schema.sql: 5 = Information Technology, 1 = Finance, 3 = Operations. EmployeeId is generated automatically (IDENTITY), so it is not supplied.
-- ============================================================================================================

INSERT INTO Employee (FirstName, LastName, EmailAddress, DepartmentId) VALUES
 ('Aileen', 'MacLeod', 'aileen.macleod@scottishglen.example', 5), -- IT
 ('Callum', 'Reid',    'callum.reid@scottishglen.example',    1), -- FINANCE
 ('Isla',   'Murray',  'isla.murray@scottishglen.example',    3); -- OPERATIONS