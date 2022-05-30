CREATE PROCEDURE SP_GetRate
(
@JobType Varchar(50) = ''
)
AS
BEGIN
SELECT RATE FROM RATE
where JOB_TYPE = @JobType
END