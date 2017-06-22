IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'NeedItemClone')
BEGIN
	PRINT 'Dropping Procedure NeedItemClone'
	DROP  Procedure NeedItemClone
END
GO

PRINT 'Creating Procedure NeedItemClone'
GO

/*********************************************************************************************
**		File: 
**		Name: NeedItemClone
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

CREATE Procedure dbo.NeedItemClone
(
		@NeedItemId				INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT							
	,	@Name					VARCHAR(50)							
	,	@Description            VARCHAR (500)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'NeedItem'				
)

AS

BEGIN

	IF @NeedItemId IS NULL OR @NeedItemId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'NeedItem', @NeedItemId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Name				= Name
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.NeedItem
	WHERE	NeedItemId		= @NeedItemId  
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.NeedItemInsert 
			@NeedItemId			=	NULL
		,	@ApplicationId		=	ApplicationId
		,	@Name				=	@Name
		,	@Description		=	@Description
		,	@SortOrder			=	@SortOrder
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert		
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @NeedItemId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
