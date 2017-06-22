	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestRunDelete')
BEGIN
	PRINT 'Dropping Procedure TestRunDelete'
	DROP  Procedure TestRunDelete
END
GO

PRINT 'Creating Procedure TestRunDelete'
GO
/******************************************************************************
**		File: 
**		Name: TestRunDelete
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
CREATE Procedure dbo.TestRunDelete
(
		@TestRunId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TestRun'	
)
AS
BEGIN

	DELETE	 dbo.TestRun
	WHERE	 TestRunId = @TestRunId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestRunId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
