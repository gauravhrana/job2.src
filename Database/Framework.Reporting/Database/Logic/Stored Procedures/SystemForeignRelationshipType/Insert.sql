IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipTypeInsert')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipTypeInsert'
	DROP  Procedure SystemForeignRelationshipTypeInsert
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SystemForeignRelationshipTypeInsert
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
CREATE Procedure dbo.SystemForeignRelationshipTypeInsert
(
		@SystemForeignRelationshipTypeId		INT				= NULL 	OUTPUT	
	,	@ApplicationId							INT				
	,	@Name									VARCHAR(50)						
	,	@Description							VARCHAR (500)						
	,	@SortOrder								INT								
	,	@AuditId								INT				
	,	@TraceId								INT				= NULL					
	,	@AuditDate								DATETIME		= NULL				
	,	@SystemEntityType						VARCHAR(50)		= 'SystemForeignRelationshipType'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SystemForeignRelationshipTypeId OUTPUT, @AuditId
		
	INSERT INTO dbo.SystemForeignRelationshipType 
	( 
			SystemForeignRelationshipTypeId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@SystemForeignRelationshipTypeId
		,	@ApplicationId			
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	SELECT @SystemForeignRelationshipTypeId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SystemForeignRelationshipTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId		
		,	@TraceId				= @TraceId
END	
GO

 