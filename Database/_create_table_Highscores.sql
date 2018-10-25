USE DOCBILL;
GO

--drop tables to take care of foreign keys
--DROP TABLE IF EXISTS dbo.Highscores;
--DROP TABLE IF EXISTS dbo.Passwords;
--DROP TABLE IF EXISTS dbo.Users;
--GO

--Highscores table
DROP TABLE IF EXISTS dbo.Highscores;
GO
CREATE TABLE dbo.Highscores
(
    HighscoreID             INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    UserID                  INT NOT NULL FOREIGN KEY REFERENCES dbo.Users(UserID),
    Score                   INT NOT NULL DEFAULT 0
);
GO
