IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND Name = 'TestSuiteXTestCaseArchiveDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseArchiveDeleteHard'
	DROP  Procedure TestSuiteXTestCaseArchiveDeleteHard
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseArchiveDeleteHard'
GO
/******************************************************************************
**		Task: 
**		SystemEntityTypeId: TestSuiteXTestCaseArchiveDelete
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
**		Date:		Author:				AssignedTo:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.TestSuiteXTestCaseArchiveDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(200)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'TestSuiteXTestCaseArchive'	 
)
AS
BEGIN

	IF @KeyType = 'TestSuiteXTestCaseArchiveId'
		BEGIN

			DELETE	 dbo.TestSuiteXTestCaseArchive
			WHERE	 TestSuiteXTestCaseArchiveId = @KeyId

		END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
