IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCasePriorityInsert')
BEGIN
	PRINT 'Dropping Procedure TestCasePriorityInsert'
	DROP  Procedure TestCasePriorityInsert
END
GO

PRINT 'Creating Procedure TestCasePriorityInsert'
GO

/*********************************************************************************************
**		File: 
**		Name:TestCasePriorityInsert
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

CREATE Procedure dbo.TestCasePriorityInsert
(
		@TestCasePriorityId			INT			= NULL 	OUTPUT		
	,	@ApplicationId			INT	
	,	@Name					VARCHAR(50)						
	,	@Description			VARCHAR(100)						
	,	@SortOrder				INT								
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50) = 'TestCasePriority'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TestCasePriorityId OUTPUT, @AuditId
		
	INSERT INTO dbo.TestCasePriority 
	( 
			TestCasePriorityId
		,	ApplicationId						
		,	Name				
		,	Description				
		,	SortOrder						
	)
	VALUES 
	(  
			@TestCasePriorityId
		,	@ApplicationId		
		,	@Name				
		,	@Description		
		,	@SortOrder			
	)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestCasePriorityId
		,	@AuditAction			= 'Insert'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO

 