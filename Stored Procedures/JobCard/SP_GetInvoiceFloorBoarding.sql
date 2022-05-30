create PROCEDURE SP_GetInvoiceFloorBoarding
(
	@JobCardNo int = 0
)
AS
BEGIN
select j.JOB_CARD_NO as [JOB_CARD_NO],j.NO_OF_DAYS as [NO_OF_DAYS],e.EMPLOYEE_NO as [EmpNo], e.NAME as [Employee Name],e.SURNAME as [Employee Surname],c.NAME as [Customer Name],c.SURNAME as [Customer Surname],c.ADDRESS_LINE_ONE as [Add One],c.ADDRESS_LINE_TWO as [Add two],c.CITY as [city],c.POSTAL_CODE as [code], r.JOB_TYPE as [Job Type],eq.STANDARD_FLOOR_BOARDS as [FloorBoards],'R' + CAST(r.RATE AS VARCHAR(15)) as [Rate], 'R' + CAST(r.RATE * j.NO_OF_DAYS AS VARCHAR(15)) AS Subtotal, 'R' + CAST(0.14*r.RATE * j.NO_OF_DAYS AS VARCHAR(15)) AS [VAT], 'R' + CAST(((0.14*r.RATE * j.NO_OF_DAYS) + (RATE * j.NO_OF_DAYS)) AS  VARCHAR(15)) AS [Total:]  from job j
	left join EMPLOYEE_JOB_CARD ejc on ejc.JOB_CARD_NO = j.JOB_CARD_NO
	left join EMPLOYEE e on ejc.EMPLOYEE_NO = e.EMPLOYEE_NO
	inner join QUOTATION q on q.JOB_CARD_NO = j.JOB_CARD_NO
	inner join CUSTOMER c on c.CUSTOMER_ID = q.CUSTOMER_ID
	inner join JOB_EQUIPTMENT_MATERIALS jec on jec.JOB_CARD_NO = j.JOB_CARD_NO
	inner join EQUIPTMENT_MATERIALS eq on eq.EQUIPTMENT_MATERIALS_ID = jec.EQUIPTMENT_MATERIALS_ID
	inner join RATE r on r.RATE_ID = eq.RATE_ID
	where j.JOB_CARD_NO = @JobCardNo
END
