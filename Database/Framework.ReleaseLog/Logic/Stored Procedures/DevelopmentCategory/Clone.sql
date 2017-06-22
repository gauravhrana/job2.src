IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DevelopmentCategoryClone')
BEGIN
	PRINT 'Dropping Procedure DevelopmentCategoryClone'
	DROP  Procedure DevelopmentCategoryClone
END
GO

PRINT 'Creating Procedure DevelopmentCategoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: DevelopmentCategoryClone
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
CREATE Procedure dbo.DevelopmentCategoryClone
(
		@DevelopmentCategoryId			INT			= NULL 	OUTPUT	
	,	@ApplicationId					INT			= NULL
	,	@Name							VARCHAR(50)						
	,	@Description					VARCHAR (500)						
	,	@SortOrder						INT	
	,	@DateCreated					DATETIME	= NULL
	,	@DateModified					DATETIME	= NULL
	,	@CreatedByAuditId				INT			= NULL
	,	@ModifiedByAuditId				INT			= NULL								
	,	@AuditId						INT									
	,	@AuditDate						DATETIME	= NULL			
	,	@SystemEntityType				VARCHAR(50)	= 'DevelopmentCategory'
)
AS
BEGIN

	IF @DevelopmentCategoryId IS NULL OR @DevelopmentCategoryId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @DevelopmentCategoryId OUTPUT
	END			
	
	SELECT	@ApplicationId					= ApplicationId
		,	@Description					= Description
		,	@SortOrder						= SortOrder	
		,	@DateCreated					= DateCreated
		,	@DateModified					= DateModified
		,	@CreatedByAuditId				= CreatedByAuditId	
		,	@ModifiedByAuditId				= ModifiedByAuditId
	FROM	dbo.DevelopmentCategory
	WHERE	DevelopmentCategoryId		= @DevelopmentCategoryId

	EXEC dbo.DevelopmentCategoryInsert 
			@DevelopmentCategoryId	=	NULL
		,	@ApplicationId				=	@ApplicationId
		,	@Name						=	@Name
		,	@Description				=	@Description
		,	@SortOrder					=	@SortOrder
		,	@DateCreated				=	@DateCreated
		,	@DateModified				=	@DateModified
		,	@CreatedByAuditId			=	@CreatedByAuditId
		,	@ModifiedByAuditId			=	@ModifiedByAuditId
		,	@AuditId					=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DevelopmentCategory'
		,	@EntityKey				= @DevelopmentCategoryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
