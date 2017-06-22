IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailInsert')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailInsert'
	DROP  Procedure ThemeDetailInsert
END
GO

PRINT 'Creating Procedure ThemeDetailInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ThemeDetailInsert
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
**********************************************************************************************/

CREATE Procedure dbo.ThemeDetailInsert
(
		@ThemeDetailId			INT			= NULL 	OUTPUT	
	,	@ApplicationId	        INT         = NULL
	,	@Value					VARCHAR(50)						
	,	@ThemeId				INT						
	,	@ThemeKeyId				INT
	,	@ThemeCategoryId		INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ThemeDetail'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ThemeDetailId OUTPUT, @AuditId
	
	INSERT INTO dbo.ThemeDetail 
	( 
			ThemeDetailId	
		,   ApplicationId					
		,	ThemeKeyId						
		,	Value					
		,	ThemeId
		,	ThemeCategoryId						
	)
	VALUES 
	(  
			@ThemeDetailId	
		,   @ApplicationId	
		,	@ThemeKeyId						
		,	@Value				
		,	@ThemeId
		,	@ThemeCategoryId		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ThemeDetailId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 