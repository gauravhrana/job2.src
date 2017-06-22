IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SuperKeyClone')
BEGIN
	PRINT 'Dropping Procedure SuperKeyClone'
	DROP  Procedure SuperKeyClone
END
GO

PRINT 'Creating Procedure SuperKeyClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SuperKeyClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.SuperKeyClone
(
		@SuperKeyId				INT			= NULL 	OUTPUT	
	,   @ApplicationId			INT				= NULL	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT			
	,	@SystemEntityTypeId		INT			
	,	@ExpirationDate			DATETIME							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SuperKey'
)
AS
BEGIN					
	
	SELECT	@ApplicationId			= ApplicationId
		,	@Description			= Description
		,	@SortOrder				= SortOrder	
		,	@SystemEntityTypeId		= SystemEntityTypeId
		,	@ExpirationDate			= ExpirationDate				
	FROM	dbo.SuperKey
	WHERE   SuperKeyId				= @SuperKeyId
	ORDER BY SuperKeyId

	EXEC dbo.SuperKeyInsert 
			@SuperKeyId				=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@SystemEntityTypeId		=	@SystemEntityTypeId
		,	@ExpirationDate			=	@ExpirationDate
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKey'
		,	@EntityKey				= @SuperKeyId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
