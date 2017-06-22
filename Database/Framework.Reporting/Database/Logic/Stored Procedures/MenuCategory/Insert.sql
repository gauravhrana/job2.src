IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryInsert'
	DROP  Procedure MenuCategoryInsert
END
GO

PRINT 'Creating Procedure MenuCategoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:MenuCategoryInsert
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

CREATE Procedure dbo.MenuCategoryInsert
(
		@MenuCategoryId			INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'MenuCategory'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MenuCategoryId OUTPUT, @AuditId
	
	INSERT INTO dbo.MenuCategory 
	( 
			MenuCategoryId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder						
	)
	VALUES 
	(  
			@MenuCategoryId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 