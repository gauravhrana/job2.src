IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NotificationRegistrarPublishDetails')
BEGIN
	PRINT 'Dropping Procedure NotificationRegistrarPublishDetails'
	DROP  Procedure NotificationRegistrarPublishDetails
END
GO

PRINT 'Creating Procedure NotificationRegistrarPublishDetails'
GO
/******************************************************************************
**		File: 
**		Name: NotificationRegistrarPublishDetails
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Developer:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE procedure NotificationRegistrarPublishDetails
(
	
			@CurrentPublishdate					DATETIME		= NULL	
   		,	@MaxPublishDate					DECIMAL		= NULL	
)
AS
BEGIN
IF @MaxPublishDate = 0

BEGIN
SELECT max(CAST(PublishDateId AS VARCHAR(10) ) + CAST(PublishTImeId AS VARCHAR(10)) ) AS MaxPublishDate FROM NotificationRegistrar
END
ELSE
BEGIN

	DECLARE @CompareMaxPublishDate float =@MaxPublishDate  
	DECLARE @CurrentPublishdate1 DATETIME=Getdate()
  	DECLARE @CompareCurrentPublishDate INT = CONVERT(VARCHAR(30), @CurrentPublishdate1, 112)
	DECLARE @CompareCurrentPublishTime INT = Replace(convert(varchar(5),@CurrentPublishdate1,108),':','')+'00'
	
	DECLARE @CompareDate Float = CAST(@CompareCurrentPublishDate AS VARCHAR(8) ) + CAST(@CompareCurrentPublishTime AS VARCHAR(6))
	   
SELECT count(*) AS Count,@CompareDate AS lastDate FROM NotificationRegistrar WHERE 
CAST(PublishDateId AS VARCHAR(10) ) + CAST(PublishTImeId AS VARCHAR(10)) <= @CompareDate 
AND CAST(PublishDateId AS VARCHAR(10) ) + CAST(PublishTImeId AS VARCHAR(10)) >= @CompareMaxPublishDate
  END
END




	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'NotificationRegistrar'
		,	@EntityKey				= @NotificationRegistrarId
		,	@AuditAction			= 'PublishDetails' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
