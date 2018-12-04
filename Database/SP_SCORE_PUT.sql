USE DOCBILL;
GO

DROP PROCEDURE IF EXISTS dbo.SP_SCORE_PUT;
GO

CREATE PROCEDURE dbo.SP_SCORE_PUT
    @p_Username VARCHAR(100),
    @p_Score VARCHAR(10),
    @p_Misc VARCHAR(1000)
AS
BEGIN
    INSERT INTO dbo.SCORES
    SELECT
        @p_Username AS username,
        CONVERT(INT, @p_Score) AS score,
        GETDATE() AS create_dtm,
        'Y' AS active,
        @p_Misc AS misc

    RETURN 1;
END
GO

IF OBJECT_ID('dbo.SP_SCORE_PUT') IS NOT NULL
BEGIN
    PRINT '<<< CREATED PROCEDURE SP_SCORE_PUT >>>';
END
GO
