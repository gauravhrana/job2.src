IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestSuiteDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TestSuiteDeleteHard'
	DROP  Procedure TestSuiteDeleteHard
END
GO

PRINT 'Creating Procedure TestSuiteDeleteHard'
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
CREATE Procedure dbo.TestSuiteDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestSuite'
)
AS
BEGIN

	IF @KeyType = 'TestSuiteId'
	BEGIN

		EXEC	dbo.TestSuiteDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TestSuiteId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TestSuite
		WHERE	 TestSuiteId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
