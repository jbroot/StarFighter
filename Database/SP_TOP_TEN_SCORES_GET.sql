USE DOCBILL;
GO

DROP PROCEDURE IF EXISTS dbo.SP_TOP_TEN_SCORES_GET;
GO

CREATE PROCEDURE dbo.SP_TOP_TEN_SCORES_GET
    @p_Date VARCHAR(20)
AS
BEGIN
    CREATE TABLE #TOP_TEN
    (
        id                    INT IDENTITY(0,1),
        username              VARCHAR(100) NULL,
        score                 INT NULL
    );
    
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score
    INSERT INTO #TOP_TEN SELECT NULL AS username, 0 AS score

    INSERT INTO #TOP_TEN
    SELECT DISTINCT
        username AS username,
        score AS score
    FROM dbo.SCORES
    WHERE active IN('Y')

    SELECT TOP 10
        COALESCE(username, '...') AS username,
        COALESCE(score, 0) AS score
    FROM #TOP_TEN
    ORDER BY score DESC, username DESC

    DROP TABLE #TOP_TEN

    RETURN 0;
END
GO

IF OBJECT_ID('dbo.SP_TOP_TEN_SCORES_GET') IS NOT NULL
BEGIN
    PRINT '<<< CREATED PROCEDURE SP_TOP_TEN_SCORES_GET >>>';
END
GO
