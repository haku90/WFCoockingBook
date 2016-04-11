Use Master
Go
IF EXISTS (SELECT *
FROM master..sysdatabases
WHERE name = N'PersistenceDatabase')
DROP DATABASE PersistenceDatabase
GO
CREATE DATABASE PersistenceDatabase
GO