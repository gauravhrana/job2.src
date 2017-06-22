IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskEntityTypeInsert')
BEGIN
	PRINT 'Dropping Procedure TaskEntityTypeInsert'
	DROP  Procedure TaskEntityTypeInsert
END
GO

PRINT 'Creating Procedure TaskEntityTypeInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TaskEntityTypeInsert
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

CREATE Procedure dbo.TaskEntityTypeInsert
(
		@TaskEntityTypeId		INT				= NULL 	OUTPUT	
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(50)						
	,	@Active					INT								
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME		= NULL				
	,	@SystemEntityType		VARCHAR(50)		= 'TaskEntityType'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TaskEntityTypeId OUTPUT, @AuditId
		
	INSERT INTO dbo.TaskEntityType 
	( 
			TaskEntityTypeId	
		,	ApplicationId				
		,	Name				
		,	Description			
		,	Active				
		,	SortOrder						
	)
	VALUES 
	(  
			@TaskEntityTypeId	
		,	@ApplicationId
		,	@Name				
		,	@Description		
		,	@Active				
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityTypeId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 