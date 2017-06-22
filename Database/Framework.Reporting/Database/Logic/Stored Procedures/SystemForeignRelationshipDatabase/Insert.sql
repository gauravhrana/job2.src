IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SystemForeignRelationshipDatabaseInsert')
BEGIN
	PRINT 'Dropping Procedure SystemForeignRelationshipDatabaseInsert'
	DROP  Procedure SystemForeignRelationshipDatabaseInsert
END
GO

PRINT 'Creating Procedure SystemForeignRelationshipDatabaseInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:SystemForeignRelationshipDatabaseInsert
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
CREATE Procedure dbo.SystemForeignRelationshipDatabaseInsert
(
		@SystemForeignRelationshipDatabaseId		INT				= NULL 	OUTPUT	
	,	@ApplicationId								INT				
	,	@Name										VARCHAR(50)						
	,	@Description								VARCHAR (500)						
	,	@SortOrder									INT								
	,	@AuditId									INT				
	,	@TraceId									INT				= NULL					
	,	@AuditDate									DATETIME		= NULL				
	,	@SystemEntityType							VARCHAR(50)		= 'SystemForeignRelationshipDatabase'
)
AS
BEGIN
	
	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SystemForeignRelationshipDatabaseId OUTPUT, @AuditId
		
	INSERT INTO dbo.SystemForeignRelationshipDatabase 
	( 
			SystemForeignRelationshipDatabaseId
		,	ApplicationId							
		,	Name				
		,	Description			
		,	SortOrder						
	)
	VALUES 
	(  
			@SystemForeignRelationshipDatabaseId
		,	@ApplicationId			
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	SELECT @SystemForeignRelationshipDatabaseId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SystemForeignRelationshipDatabaseId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId		
		,	@TraceId				= @TraceId
END	
GO

 