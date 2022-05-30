CREATE PROCEDURE SP_AddEmployeeToJobCard
(
@EmployeeNo VARCHAR(6) ='',
@JobCard int = 0
)
AS
BEGIN
	INSERT INTO EMPLOYEE_JOB_CARD(EMPLOYEE_NO,JOB_CARD_NO)
	VALUES (@EmployeeNo,@JobCard)
END