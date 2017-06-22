	IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteDelete')
BEGIN
	PRINT 'Dropping Procedure TestSuiteDelete'
	DROP  Procedure TestSuiteDelete
END
GO

PRINT 'Creating Procedure TestSuiteDelete'
GO
/******************************************************************************
**		File: 
**		Name: TestSuiteDelete
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
CREATE Procedure dbo.TestSuiteDelete
(
		@TestSuiteId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50) = 'TestSuite'	
)
AS
BEGIN

	DELETE	 dbo.TestSuite
	WHERE	 TestSuiteId = @TestSuiteId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TestSuiteId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
