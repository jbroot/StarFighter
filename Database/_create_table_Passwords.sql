USE DOCBILL;
GO

--drop tables to take care of foreign keys
--DROP TABLE IF EXISTS dbo.Highscores;
--DROP TABLE IF EXISTS dbo.Passwords;
--DROP TABLE IF EXISTS dbo.Users;
--GO

--Passwords table
DROP TABLE IF EXISTS dbo.Passwords;
GO
CREATE TABLE dbo.Passwords
(
    PasswordID              INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    UserID                  INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserID),
    [Password]              VARCHAR(100) NOT NULL
);
GO
