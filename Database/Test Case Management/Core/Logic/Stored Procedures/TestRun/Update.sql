IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestRunUpdate')
BEGIN
	PRINT 'Dropping Procedure TestRunUpdate'
	DROP  Procedure  TestRunUpdate
END
GO

PRINT 'Creating Procedure TestRunUpdate'
GO

/******************************************************************************
**		File: 
**		Name: TestRunUpdate
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

CREATE Procedure dbo.TestRunUpdate
(
		@TestRunId			INT		
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(100)			
	,	@SortOrder				INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50) = 'TestRun'
)
AS
BEGIN 

	UPDATE	dbo.TestRun 
	SET		Name				=	@Name				
		,	Description			=	@Description			
		,	SortOrder			=	@SortOrder							
	WHERE	TestRunId			=	@TestRunId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @TestRunId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO