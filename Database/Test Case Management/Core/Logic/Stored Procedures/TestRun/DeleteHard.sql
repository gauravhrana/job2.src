IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'TestRunDeleteHard')
BEGIN
	PRINT 'Dropping Procedure TestRunDeleteHard'
	DROP  Procedure TestRunDeleteHard
END
GO

PRINT 'Creating Procedure TestRunDeleteHard'
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
CREATE Procedure dbo.TestRunDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'TestRun'
)
AS
BEGIN

	IF @KeyType = 'TestRunId'
	BEGIN

		EXEC	dbo.TestRunDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'TestRunId',
				@AuditId	=	@AuditId

		DELETE	 dbo.TestRun
		WHERE	 TestRunId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
