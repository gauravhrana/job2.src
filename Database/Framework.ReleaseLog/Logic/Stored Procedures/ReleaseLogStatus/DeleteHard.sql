IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ReleaseLogStatusDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogStatusDeleteHard'
	DROP  Procedure ReleaseLogStatusDeleteHard
END
GO

PRINT 'Creating Procedure ReleaseLogStatusDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ReleaseLogStatusDelete
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
CREATE Procedure dbo.ReleaseLogStatusDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'ReleaseLogStatus'	
)
AS
BEGIN
		IF @KeyType = 'ReleaseLogStatusId'
		BEGIN

		EXEC	@KeyId		=	@KeyId 
				@KeyType	=	'ReleaseLogStatusId'  
			,	@AuditId	=	@AuditId

		DELETE	 dbo.ReleaseLogStatus
		WHERE	 ReleaseLogStatusId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
