IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeClone')
BEGIN
	PRINT 'Dropping Procedure ThemeClone'
	DROP  Procedure ThemesClone
END
GO

PRINT 'Creating Procedure ThemeClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ThemeClone
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

CREATE Procedure dbo.ThemesClone
(
		@ThemeId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'Theme'
)
AS
BEGIN

	IF @ThemeId IS NULL OR @ThemeId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ThemeId OUTPUT
	END						
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.Themes
	WHERE   ThemeId		=	@ThemeId
	ORDER BY ThemeId

	EXEC dbo.ThemesInsert 
			@ThemeId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Theme'
		,	@EntityKey				= @ThemeId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
