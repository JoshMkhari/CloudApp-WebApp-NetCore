CREATE PROCEDURE SP_UpdateRate
(
	@JobType Varchar(50) = '',
	@Rate int= 0
)
AS
BEGIN
	UPDATE RATE
	SET RATE = @Rate
	WHERE JOB_TYPE = @JobType
END
