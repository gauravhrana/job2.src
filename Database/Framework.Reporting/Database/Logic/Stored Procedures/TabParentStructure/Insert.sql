IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabParentStructureInsert')
BEGIN
	PRINT 'Dropping Procedure TabParentStructureInsert'
	DROP  Procedure TabParentStructureInsert
END
GO

PRINT 'Creating Procedure TabParentStructureInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TabParentStructureInsert
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

CREATE Procedure dbo.TabParentStructureInsert
(
		@TabParentStructureId		INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT			
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT
	,	@IsAllTab					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'TabParentStructure'
)
AS
BEGIN

    EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TabParentStructureId OUTPUT, @AuditId
	
	INSERT INTO dbo.TabParentStructure 
	( 
			TabParentStructureId	
		,   ApplicationId					
		,	Name						
		,	Description					
		,	SortOrder
		,	IsAllTab						
	)
	VALUES 
	(  
			@TabParentStructureId	
		,   @ApplicationId	
		,	@Name						
		,	@Description				
		,	@SortOrder	
		,	@IsAllTab		
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TabParentStructureId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 