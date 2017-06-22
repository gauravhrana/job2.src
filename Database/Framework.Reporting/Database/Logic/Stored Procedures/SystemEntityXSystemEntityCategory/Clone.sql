IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityXSystemEntityCategoryClone')
BEGIN
	PRINT 'Dropping Procedure SystemEntityXSystemEntityCategoryClone'
	DROP  Procedure SystemEntityXSystemEntityCategoryClone
END
GO

PRINT 'Creating Procedure SystemEntityXSystemEntityCategoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SystemEntityXSystemEntityCategoryClone
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
CREATE Procedure dbo.SystemEntityXSystemEntityCategoryClone
(
		@SystemEntityXSystemEntityCategoryId			INT			= NULL 	OUTPUT		
	,	@SystemEntityId					INT								
	,	@SystemEntityCategoryId					INT		
	,	@ApplicationId				INT							
	,	@AuditId					INT									
	,	@AuditDate					DATETIME	= NULL				
	,	@SystemEntityType			VARCHAR(50)	= 'SystemEntityXSystemEntityCategory'				
)
AS
BEGIN

	IF @SystemEntityXSystemEntityCategoryId IS NULL OR @SystemEntityXSystemEntityCategoryId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'SystemEntityXSystemEntityCategory', @SystemEntityXSystemEntityCategoryId OUTPUT
	END			
	
	SELECT	@SystemEntityId				= SystemEntityId
		,	@SystemEntityCategoryId				= SystemEntityCategoryId	
		,	@ApplicationId			= ApplicationId			
	FROM	dbo.SystemEntityXSystemEntityCategory
	WHERE	SystemEntityXSystemEntityCategoryId		= @SystemEntityXSystemEntityCategoryId

	EXEC dbo.SystemEntityXSystemEntityCategoryInsert 
			@SystemEntityXSystemEntityCategoryId		=	NULL
		,	@SystemEntityId				=	@SystemEntityId
		,	@SystemEntityCategoryId				=	@SystemEntityCategoryId
		,	@ApplicationId			=	@ApplicationId
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemEntityXSystemEntityCategory'
		,	@EntityKey				= @SystemEntityXSystemEntityCategoryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
