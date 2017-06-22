IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TabChildStructureInsert')
BEGIN
	PRINT 'Dropping Procedure TabChildStructureInsert'
	DROP  Procedure TabChildStructureInsert
END
GO

PRINT 'Creating Procedure TabChildStructureInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TabChildStructureInsert
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
**		Date:		Author:				EntityName:
**		--------	--------			------------------------------------------------------
**********************************************************************************************/

CREATE Procedure dbo.TabChildStructureInsert
(
		@TabChildStructureId		INT				= NULL 	OUTPUT	
	,   @ApplicationId				INT				= NULL	
	,	@Name						VARCHAR(50)						
	,	@EntityName					VARCHAR(50)						
	,	@SortOrder					INT
	,	@TabParentStructureId		INT	
	,	@InnerControlPath			VARCHAR(200)	
	,	@AuditId					INT									
	,	@AuditDate					DATETIME		= NULL				
	,	@SystemEntityType			VARCHAR(50)		= 'TabChildStructure'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TabChildStructureId OUTPUT, @AuditId
	
	INSERT INTO dbo.TabChildStructure 
	( 
			TabChildStructureId	
		,   ApplicationId					
		,	Name						
		,	EntityName					
		,	SortOrder
		,	TabParentStructureId
		,	InnerControlPath						
	)
	VALUES 
	(  
			@TabChildStructureId	
		,   @ApplicationId	
		,	@Name						
		,	@EntityName				
		,	@SortOrder	
		,	@TabParentStructureId	
		,	@InnerControlPath	
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TabChildStructureId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
END	
GO

 