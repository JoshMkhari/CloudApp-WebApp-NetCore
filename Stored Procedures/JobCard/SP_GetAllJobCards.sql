CREATE PROCEDURE SP_GetAllJobCards
AS
BEGIN
	select j.JOB_CARD_NO as [JOB_CARD_NO],j.NO_OF_DAYS as [NO_OF_DAYS], COUNT(ejc.EMPLOYEE_NO) as [Employees Assigned] from job j
	left join EMPLOYEE_JOB_CARD ejc on ejc.JOB_CARD_NO = j.JOB_CARD_NO
	group by j.JOB_ID,j.JOB_CARD_NO,j.NO_OF_DAYS
	order by COUNT(ejc.EMPLOYEE_NO) ASC
END
