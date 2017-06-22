IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ThemeCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure ThemeCategoryInsert'
	DROP  Procedure ThemeCategoryInsert
END
GO

PRINT 'Creating Procedure ThemeCategoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ThemeCategoryInsert
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

CREATE Procedure dbo.ThemeCategoryInsert
(
		@ThemeCategoryId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'ThemeCategory'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ThemeCategoryId OUTPUT, @AuditId
	
	INSERT INTO dbo.ThemeCategory 
	( 
			ThemeCategoryId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@ThemeCategoryId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ThemeCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 