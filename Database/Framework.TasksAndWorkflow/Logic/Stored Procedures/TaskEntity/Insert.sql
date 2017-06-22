IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityInsert')
BEGIN
	PRINT 'Dropping Procedure TaskEntityInsert'
	DROP  Procedure TaskEntityInsert
END
GO

PRINT 'Creating Procedure TaskEntityInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TaskEntityInsert
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

CREATE Procedure dbo.TaskEntityInsert
(
		@TaskEntityId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@TaskEntityTypeId		INT								
	,	@Description			VARCHAR(50)						
	,	@Active					INT								
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'TaskEntity'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TaskEntityId OUTPUT, @AuditId
		
	INSERT INTO dbo.TaskEntity 
	( 
			TaskEntityId
		,	ApplicationId						
		,	Name				
		,	TaskEntityTypeId	
		,	Description			
		,	Active				
		,	SortOrder						
	)
	VALUES 
	(  
			@TaskEntityId
		,	@ApplicationId		
		,	@Name				
		,	@TaskEntityTypeId	
		,	@Description		
		,	@Active				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 