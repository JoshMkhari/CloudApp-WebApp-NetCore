CREATE PROCEDURE SP_EmployeesWorkingOnJob
(
	@JobCardNo int = 0
)
AS
BEGIN
Select  ej.EMPLOYEE_NO as EmpNo, e.NAME as [Employee Name], e.SURNAME as [Employee Surname] from EMPLOYEE_JOB_CARD ej
inner join EMPLOYEE e on e.EMPLOYEE_NO = ej.EMPLOYEE_NO 
where JOB_CARD_NO = @JobCardNo
END