CREATE PROCEDURE SP_UpdateEmployee
(
	@EMPLOYEE_NO VARCHAR (6)  = '',
	@NAME VARCHAR (50) = '',
	@SURNAME VARCHAR (50)  = ''
)
AS
BEGIN
	UPDATE EMPLOYEE
	SET [NAME] = @NAME,
		SURNAME = @SURNAME
	WHERE EMPLOYEE_NO = @EMPLOYEE_NO
END