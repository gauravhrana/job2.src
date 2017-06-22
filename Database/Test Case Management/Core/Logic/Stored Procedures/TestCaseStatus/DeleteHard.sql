IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseStatusDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TestCaseStatusDeleteHard'
	DROP  Procedure TestCaseStatusDeleteHard
END
GO

PRINT 'Creating Procedure TestCaseStatusDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TestCaseStatusDelete
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
CREATE Procedure dbo.TestCaseStatusDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestCaseStatus'
)
AS
BEGIN

	IF @KeyType = 'TestCaseStatusId'
	BEGIN

		EXEC	dbo.TestCaseStatusDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TestCaseStatusId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TestCaseStatus
		WHERE	 TestCaseStatusId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
