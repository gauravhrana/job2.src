IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailsClone')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailsClone'
	DROP  Procedure ThemeDetailsClone
END
GO

PRINT 'Creating Procedure ThemeDetailsClone'
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

CREATE Procedure dbo.ThemeDetailsClone
(
		@ThemeDetailId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Value					VARCHAR(50)						
	,	@ThemeId				INT						
	,	@ThemeKeyId				INT
	,	@ThemeCategoryId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeDetails'
)
AS
BEGIN

	IF @ThemeDetailId IS NULL OR @ThemeId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ThemeDetailId OUTPUT
	END						
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@ThemeId			=	ThemeId
		,	@ThemeKeyId			=	ThemeKeyId
		,	@ThemeCategoryId	=	ThemeCategoryId
		,	@Value				=	Value				
	FROM	dbo.ThemeDetails
	WHERE   ThemeDetailId		=	@ThemeDetailId
	ORDER BY ThemeDetailId

	EXEC dbo.ThemeDetailsInsert 
			@ThemeDetailId		=	NULL
		,   @ApplicationId		=   @ApplicationId
		,	@ThemeId			=	@ThemeId
		,	@ThemeKeyId			=	@ThemeKeyId
		,	@ThemeCategoryId	=	@ThemeCategoryId
		,	@Value				=	@Value	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeDetails'
		,	@EntityKey				= @ThemeDetailId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
