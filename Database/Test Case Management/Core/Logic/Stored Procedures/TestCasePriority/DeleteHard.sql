IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestCasePriorityDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TestCasePriorityDeleteHard'
	DROP  Procedure TestCasePriorityDeleteHard
END
GO

PRINT 'Creating Procedure TestCasePriorityDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: TestCasePriorityDelete
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
CREATE Procedure dbo.TestCasePriorityDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestCasePriority'
)
AS
BEGIN

	IF @KeyType = 'TestCasePriorityId'
	BEGIN

		EXEC	dbo.TestCasePriorityDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TestCasePriorityId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TestCasePriority
		WHERE	 TestCasePriorityId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
