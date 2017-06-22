IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeCategoryClone')
BEGIN
	PRINT 'Dropping Procedure ThemeCategoryClone'
	DROP  Procedure ThemeCategoryClone
END
GO

PRINT 'Creating Procedure ThemeCategoryClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ThemeCategoryClone
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

CREATE Procedure dbo.ThemeCategoryClone
(
		@ThemeCategoryId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeCategory'
)
AS
BEGIN

	IF @ThemeCategoryId IS NULL OR @ThemeCategoryId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ThemeCategoryId OUTPUT
	END						
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.ThemeCategory
	WHERE   ThemeCategoryId		=	@ThemeCategoryId
	ORDER BY ThemeCategoryId

	EXEC dbo.ThemeCategoryInsert 
			@ThemeCategoryId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeCategory'
		,	@EntityKey				= @ThemeCategoryId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
