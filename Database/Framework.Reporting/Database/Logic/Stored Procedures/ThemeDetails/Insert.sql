IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeDetailsInsert')
BEGIN
	PRINT 'Dropping Procedure ThemeDetailsInsert'
	DROP  Procedure ThemeDetailsInsert
END
GO

PRINT 'Creating Procedure ThemeDetailsInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ThemeDetailsInsert
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

CREATE Procedure dbo.ThemeDetailsInsert
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

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ThemeDetailId OUTPUT, @AuditId
	
	INSERT INTO dbo.ThemeDetails 
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

 