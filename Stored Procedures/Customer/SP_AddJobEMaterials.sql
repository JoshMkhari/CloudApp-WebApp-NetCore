CREATE PROCEDURE SP_AddJobEMaterials
(
@EquiptmentMaterialsID int = 0,
@JobCardNo int = 0
)
AS
BEGIN
	INSERT INTO JOB_EQUIPTMENT_MATERIALS(EQUIPTMENT_MATERIALS_ID,JOB_CARD_NO)
	VALUES (@EquiptmentMaterialsID,@JobCardNo)
END