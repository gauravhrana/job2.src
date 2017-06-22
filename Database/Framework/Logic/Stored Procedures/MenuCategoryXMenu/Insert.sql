IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'MenuCategoryXMenuInsert')
BEGIN
	PRINT 'Dropping Procedure MenuCategoryXMenuInsert'
	DROP  Procedure MenuCategoryXMenuInsert
END
GO

PRINT 'Creating Procedure MenuCategoryXMenuInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:MenuCategoryXMenuInsert
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

CREATE Procedure dbo.MenuCategoryXMenuInsert
(
		@MenuCategoryXMenuId		INT			= NULL 	OUTPUT	
	,	@ApplicationId								INT			= NULL	
	,	@MenuId						INT								
	,	@MenuCategoryId							INT								
	,	@AuditId									INT									
	,	@AuditDate									DATETIME	= NULL				
	,	@SystemEntityType							VARCHAR(50)	= 'MenuCategoryXMenu'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @MenuCategoryXMenuId OUTPUT, @AuditId
	
	INSERT INTO dbo.MenuCategoryXMenu 
	( 
			MenuCategoryXMenuId		
		,	ApplicationId			
		,	MenuId					
		,	MenuCategoryId						
	)
	VALUES 
	(  
			@MenuCategoryXMenuId		
		,	@ApplicationId			
		,	@MenuId			
		,	@MenuCategoryId			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @MenuCategoryXMenuId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 