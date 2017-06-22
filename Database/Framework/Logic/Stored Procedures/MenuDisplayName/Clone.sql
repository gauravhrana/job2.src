IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuDisplayNameClone')
BEGIN
	PRINT 'Dropping Procedure MenuDisplayNameClone'
	DROP  Procedure MenuDisplayNameClone
END
GO

PRINT 'Creating Procedure MenuDisplayNameClone'
GO

/*********************************************************************************************
**		File: 
**		Name: MenuDisplayNameClone
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

CREATE Procedure dbo.MenuDisplayNameClone
(
		@MenuDisplayNameId		INT			= NULL OUTPUT
	,	@ApplicationId			INT		
	,	@LanguageId				INT	
	,	@MenuId					INT	
	,	@Value					VARCHAR(50)		
	,	@IsDefault				INT						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'MenuDisplayName'
)
AS
BEGIN

	IF @MenuDisplayNameId IS NULL OR @MenuDisplayNameId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, 'MenuDisplayName', @MenuDisplayNameId OUTPUT
	END			
	
	SELECT	@ApplicationId  =	ApplicationId
		,	@Value			=	Value				
		,	@MenuId			=	MenuId				
		,	@LanguageId		=	LanguageId	
		,	@IsDefault		=	IsDefault								
	FROM	dbo.MenuDisplayName 
	WHERE	MenuDisplayNameId = @MenuDisplayNameId

	EXEC dbo.MenuDisplayNameInsert 
			@MenuDisplayNameId	=	NULL
		,	@ApplicationId		=	@ApplicationId
		,	@Value				=	@Value
		,	@MenuId				=	@MenuId
		,	@LanguageId			=	@LanguageId
		,	@IsDefault			=	@IsDefault
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuDisplayNameId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
