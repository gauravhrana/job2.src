IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'EventTypeClone')
BEGIN
	PRINT 'Dropping Procedure EventTypeClone'
	DROP  Procedure EventTypeClone
END
GO

PRINT 'Creating Procedure EventTypeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: EventTypeClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.EventTypeClone
(
		@EventTypeId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT			
	,	@Name					VARCHAR(50)	
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'EventType'			
)
AS
BEGIN

	IF @EventTypeId IS NULL OR @EventTypeId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @EventTypeId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId	
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.EventType
	WHERE	EventTypeId			= @EventTypeId 
	AND		ApplicationId		= @ApplicationId

	EXEC dbo.EventTypeInsert 
			@EventTypeId		=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @EventTypeId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
