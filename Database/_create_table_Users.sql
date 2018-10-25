USE DOCBILL;
GO

--drop tables to take care of foreign keys
--DROP TABLE IF EXISTS dbo.Highscores;
--DROP TABLE IF EXISTS dbo.Passwords;
--DROP TABLE IF EXISTS dbo.Users;
--GO

--Users table
DROP TABLE IF EXISTS dbo.Users;
GO
CREATE TABLE dbo.Users
(
    UserID                  INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    Username                VARCHAR(100) NOT NULL,
    Email                   VARCHAR(250) NULL,
    Active                  CHAR(1) NOT NULL DEFAULT 'Y'
);
GO
