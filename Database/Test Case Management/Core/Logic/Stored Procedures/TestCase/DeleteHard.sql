IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TestCaseDeleteHard'
	DROP  Procedure TestCaseDeleteHard
END
GO

PRINT 'Creating Procedure TestCaseDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TestCaseDelete
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
CREATE Procedure dbo.TestCaseDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestCase'
)
AS
BEGIN

	IF @KeyType = 'TestCaseId'
	BEGIN

		EXEC	dbo.TestCaseDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TestCaseId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TestCase
		WHERE	 TestCaseId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
