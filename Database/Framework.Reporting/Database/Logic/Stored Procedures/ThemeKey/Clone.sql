IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeKeyClone')
BEGIN
	PRINT 'Dropping Procedure ThemeKeyClone'
	DROP  Procedure ThemeKeyClone
END
GO

PRINT 'Creating Procedure ThemeKeyClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ThemeKeyClone
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

CREATE Procedure dbo.ThemeKeyClone
(
		@ThemeKeyId		INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeKey'
)
AS
BEGIN

	IF @ThemeKeyId IS NULL OR @ThemeKeyId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ThemeKeyId OUTPUT
	END						
	
	SELECT	@ApplicationId		=	ApplicationId
		,	@Description		=	Description
		,	@SortOrder			=	SortOrder				
	FROM	dbo.ThemeKey
	WHERE   ThemeKeyId		=	@ThemeKeyId
	ORDER BY ThemeKeyId

	EXEC dbo.ThemeKeyInsert 
			@ThemeKeyId		=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'ThemeKey'
		,	@EntityKey				= @ThemeKeyId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
