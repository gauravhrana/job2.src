IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemEntityXSystemEntityCategoryInsert')
BEGIN
	PRINT 'Dropping Procedure SystemEntityXSystemEntityCategoryInsert'
	DROP  Procedure SystemEntityXSystemEntityCategoryInsert
END
GO

PRINT 'Creating Procedure SystemEntityXSystemEntityCategoryInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SystemEntityXSystemEntityCategoryInsert
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
**********************************************************************************************/

CREATE Procedure dbo.SystemEntityXSystemEntityCategoryInsert
(
		@SystemEntityXSystemEntityCategoryId			INT			= NULL 	OUTPUT		
	,	@SystemEntityId					INT								
	,	@SystemEntityCategoryId					INT						
	,	@ApplicationId				INT		
	,	@AuditId					INT									
	,	@AuditDate					DATETIME	= NULL				
	,	@SystemEntityType			VARCHAR(50)	= 'SystemEntityXSystemEntityCategory'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SystemEntityXSystemEntityCategoryId OUTPUT, @AuditId
	
	INSERT INTO dbo.SystemEntityXSystemEntityCategory 
	( 
			SystemEntityXSystemEntityCategoryId						
		,	SystemEntityId				
		,	SystemEntityCategoryId
		,	ApplicationId						
	)
	VALUES 
	(  
			@SystemEntityXSystemEntityCategoryId					
		,	@SystemEntityId			
		,	@SystemEntityCategoryId	
		,	@ApplicationId	
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SystemEntityXSystemEntityCategoryId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 