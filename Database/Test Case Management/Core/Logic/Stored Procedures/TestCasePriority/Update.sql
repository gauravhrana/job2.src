IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCasePriorityUpdate')
BEGIN
	PRINT 'Dropping Procedure TestCasePriorityUpdate'
	DROP  Procedure  TestCasePriorityUpdate
END
GO

PRINT 'Creating Procedure TestCasePriorityUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TestCasePriorityUpdate
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
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.TestCasePriorityUpdate
(
		@TestCasePriorityId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TestCasePriority'
)
AS
BEGIN 

	UPDATE	dbo.TestCasePriority 
	SET		Name				=	@Name				
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	TestCasePriorityId			=	@TestCasePriorityId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TestCasePriorityId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO