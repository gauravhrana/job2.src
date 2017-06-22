IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedItemDetails')
BEGIN
	PRINT 'Dropping Procedure NeedItemDetails'
	DROP  Procedure NeedItemDetails
END
GO

PRINT 'Creating Procedure NeedItemDetails'
GO


/******************************************************************************
**		File: 
**		Name: NeedItemDetails
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.NeedItemDetails
(
		@NeedItemId		    INT					
	,   @AuditId			INT			
    ,   @AuditDate		    DATETIME	  = NULL	
	,   @SystemEntityType	VARCHAR(50)	  = 'NeedItem'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@NeedItemId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	NeedItemId
		,   ApplicationId
		,	Name
		,	Description
		,	SortOrder	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'							
	FROM	dbo.NeedItem 
	WHERE	NeedItemId = @NeedItemId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert	
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @NeedItemId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO
   