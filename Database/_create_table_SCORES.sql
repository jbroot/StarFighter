USE DOCBILL;
GO

--SCORES table
DROP TABLE IF EXISTS dbo.SCORES;
GO
CREATE TABLE dbo.SCORES
(
    id                      INT NOT NULL IDENTITY(1,1) PRIMARY KEY CLUSTERED,
    username                VARCHAR(100) NULL,
    score                   INT NOT NULL DEFAULT 0,
    create_dtm              DATETIME NULL,
    active                  CHAR(1) DEFAULT 'Y',
    misc                    VARCHAR(MAX) NULL
);
GO
