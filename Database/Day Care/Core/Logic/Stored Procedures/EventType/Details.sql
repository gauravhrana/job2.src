IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EventTypeDetails')
BEGIN
	PRINT 'Dropping Procedure EventTypeDetails'
	DROP  Procedure EventTypeDetails
END
GO

PRINT 'Creating Procedure EventTypeDetails'
GO

/******************************************************************************
**		File: 
**		Name: EventTypeDetails
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

CREATE Procedure dbo.EventTypeDetails
(
		@EventTypeId			 INT
	,   @AuditId				 INT			
    ,   @AuditDate				 DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'EventType'
)
AS
BEGIN

	DECLARE @LastUpdatedBy		AS	VARCHAR(100)
	DECLARE @LastUpdatedDate	AS	DATETIME
	DECLARE @LastAuditAction	AS	VARCHAR(50)

	EXEC dbo.AuditHistoryLastValues
			@EntityKey				=	@EventTypeId
		,	@SystemEntityType		=	@SystemEntityType
		,	@LastUpdatedBy			=	@LastUpdatedBy			OUT
		,	@LastUpdatedDate		=	@LastUpdatedDate		OUT
		,	@LastAuditAction		=	@LastAuditAction		OUT	

	SELECT	EventTypeId			
		,	ApplicationId
		,	Name				
		,	Description		
		,	SortOrder	
		,	@LastUpdatedDate		AS	'UpdatedDate'
		,	@LastUpdatedBy			AS	'UpdatedBy'
		,	@LastAuditAction		AS	'LastAction'								
	FROM	dbo.EventType 
	WHERE	EventTypeId = @EventTypeId	

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert	
			@SystemEntityType		= @SystemEntityType 
		,	@EntityKey				= @EventTypeId
		,	@AuditAction			= 'Details'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId


END		
GO
   