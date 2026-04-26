CREATE DATABASE MyAppDb;
GO

USE MyAppDb;
GO

CREATE SCHEMA MyAppSchema;
GO

-- 1. Create Users Table
CREATE TABLE MyAppSchema.Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Email NVARCHAR(200) NOT NULL,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    PasswordHash VARBINARY(MAX) NOT NULL,
    PasswordSalt VARBINARY(MAX) NOT NULL,
    CreatedAt DATETIME2 NOT NULL DEFAULT SYSUTCDATETIME(),
    CONSTRAINT UQ_Users_Email UNIQUE (Email)
);

-- 2. Create TodoItems Table
CREATE TABLE MyAppSchema.TodoItems
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(200) NOT NULL,
    Description NVARCHAR(500) NULL,
    IsCompleted BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME2 NOT NULL DEFAULT GETDATE(),
    UserId INT NOT NULL,

    CONSTRAINT FK_TodoItems_Users FOREIGN KEY (UserId)
        REFERENCES MyAppSchema.Users(Id)
        ON DELETE CASCADE
);

-- 3. Create Index for Performance
CREATE INDEX IX_TodoItems_UserId ON MyAppSchema.TodoItems(UserId);
