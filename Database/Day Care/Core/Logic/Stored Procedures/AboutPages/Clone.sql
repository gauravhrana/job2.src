IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AboutPagesClone')
BEGIN
	PRINT 'Dropping Procedure AboutPagesClone'
	DROP  Procedure AboutPagesClone
END
GO

PRINT 'Creating Procedure AboutPagesClone'
GO

/*********************************************************************************************
**		File: 
**		Name: AboutPagesClone
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

CREATE Procedure dbo.AboutPagesClone
(
		@AboutPagesId			INT				= NULL 	OUTPUT		
	,	@ApplicationId			INT		
	,	@Description			VARCHAR (500) 
	,	@Developer				VARCHAR (100) 
	,	@JIRAId					VARCHAR (100)
	,	@Feature				VARCHAR (100)
	,	@PrimaryEntity			VARCHAR (100) 							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'AboutPages'				
)
AS
BEGIN

	IF @AboutPagesId IS NULL OR @AboutPagesId = -9999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @AboutPagesId OUTPUT
	END	
		
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Description		= Description
		,	@Developer			= Developer
		,	@JIRAId				= JIRAId
		,	@Feature			= Feature
		,	@PrimaryEntity		= PrimaryEntity				
	FROM	dbo.AboutPages
	WHERE	AboutPagesId	= @AboutPagesId 
	AND		ApplicationId	= @ApplicationId

	EXEC dbo.AboutPagesInsert 
			@AboutPagesId		=	@AboutPagesId
		,	@ApplicationId		=	@ApplicationId
		,	@Description		=	@Description
		,	@Developer			=	@Developer
		,	@JIRAId				=	@JIRAId
		,	@Feature			=	@Feature
		,	@PrimaryEntity		=	@PrimaryEntity	
		,	@AuditId			=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert	
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @AboutPagesId
		,	@AuditAction			= 'Clone' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
