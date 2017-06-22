IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TaskNotesInsert')
BEGIN
	PRINT 'Dropping Procedure TaskNotesInsert'
	DROP  Procedure TaskNotesInsert
END
GO

PRINT 'Creating Procedure TaskNotesInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TaskNotesInsert
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

CREATE Procedure dbo.TaskNotesInsert
(
		@TaskNotesId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'TaskNotes'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TaskNotesId OUTPUT, @AuditId
		
	INSERT INTO dbo.TaskNotes 
	( 
			TaskNotesId
		,	ApplicationId						
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@TaskNotesId
		,	@ApplicationId		
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskNotesId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 