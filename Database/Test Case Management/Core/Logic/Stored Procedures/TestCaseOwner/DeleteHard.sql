IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCaseOwnerDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TestCaseOwnerDeleteHard'
	DROP  Procedure TestCaseOwnerDeleteHard
END
GO

PRINT 'Creating Procedure TestCaseOwnerDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TestCaseOwnerDelete
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
CREATE Procedure dbo.TestCaseOwnerDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestCaseOwner'
)
AS
BEGIN

	IF @KeyType = 'TestCaseOwnerId'
	BEGIN

		EXEC	dbo.TestCaseOwnerDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TestCaseOwnerId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TestCaseOwner
		WHERE	 TestCaseOwnerId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
