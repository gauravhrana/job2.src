IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationEntityParentalHierarchyInsert')
BEGIN
	PRINT 'Dropping Procedure ApplicationEntityParentalHierarchyInsert'
	DROP  Procedure ApplicationEntityParentalHierarchyInsert
END
GO

PRINT 'Creating Procedure ApplicationEntityParentalHierarchyInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:ApplicationEntityParentalHierarchyInsert
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

CREATE Procedure dbo.ApplicationEntityParentalHierarchyInsert
(
		@ApplicationEntityParentalHierarchyId	INT			= NULL 	OUTPUT	
	,	@ApplicationId							INT			= NULL	
	,	@Name									VARCHAR(50)						
	,	@Description							VARCHAR(50)						
	,	@SortOrder								INT								
	,	@AuditId								INT									
	,	@AuditDate								DATETIME	= NULL				
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationEntityParentalHierarchy'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ApplicationEntityParentalHierarchyId OUTPUT, @AuditId
	
	INSERT INTO dbo.ApplicationEntityParentalHierarchy 
	( 
			ApplicationEntityParentalHierarchyId
		,	ApplicationId					
		,	Name			
		,	Description		
		,	SortOrder						
	)
	VALUES 
	(  
			@ApplicationEntityParentalHierarchyId
		,	@ApplicationId		
		,	@Name			
		,	@Description	
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationEntityParentalHierarchyId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 