IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteXTestCaseDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TestSuiteXTestCaseDeleteHard'
	DROP  Procedure TestSuiteXTestCaseDeleteHard
END
GO

PRINT 'Creating Procedure TestSuiteXTestCaseDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TestSuiteXTestCaseDelete
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
CREATE Procedure dbo.TestSuiteXTestCaseDeleteHard
(
		@TestSuiteXTestCaseId		INT				= NULL		
	,	@KeyId 					INT							
	,	@KeyType				VARCHAR(50)					
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'TestSuiteXTestCase'
)
AS
BEGIN

	IF @KeyType = 'TestSuiteXTestCaseId'
	BEGIN

		DELETE	 dbo.TestSuiteXTestCase
		WHERE	 TestSuiteXTestCaseId = @KeyId

	END
	ELSE IF @KeyType = 'TestSuiteId'
	BEGIN

		DELETE	 dbo.TestSuiteXTestCase
		WHERE	 TestSuiteId = @KeyId

	END
	ELSE IF @KeyType = 'TestCaseId'
	BEGIN

		DELETE	 dbo.TestSuiteXTestCase
		WHERE	 TestCaseId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
