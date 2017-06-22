IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityCategoryClone')
BEGIN
	PRINT 'Dropping Procedure SystemEntityCategoryClone'
	DROP  Procedure SystemEntityCategoryClone
END
GO

PRINT 'Creating Procedure SystemEntityCategoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SystemEntityCategoryClone
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

CREATE Procedure dbo.SystemEntityCategoryClone
(
		@SystemEntityCategoryId	INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SystemEntityCategory'
)
AS
BEGIN

	IF @SystemEntityCategoryId IS NULL OR @SystemEntityCategoryId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SystemEntityCategoryId OUTPUT
	END						
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@SortOrder			= SortOrder				
	FROM	dbo.SystemEntityCategory
	WHERE   SystemEntityCategoryId				= @SystemEntityCategoryId
	ORDER BY SystemEntityCategoryId

	EXEC dbo.SystemEntityCategoryInsert 
			@SystemEntityCategoryId	=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SystemEntityCategory'
		,	@EntityKey				= @SystemEntityCategoryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
