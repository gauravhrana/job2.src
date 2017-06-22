IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'BatchFileStatusDeleteHard')
BEGIN
	PRINT 'Dropping Procedure BatchFileStatusDeleteHard'
	DROP  Procedure BatchFileStatusDeleteHard
END
GO

PRINT 'Creating Procedure BatchFileStatusDeleteHard'
GO
/******************************************************************************
**		File: 
**		Name: BatchFileStatusDelete
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
CREATE Procedure dbo.BatchFileStatusDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'BatchFileStatus'
)
AS
BEGIN

	IF @KeyType = 'BatchFileStatusId'
	BEGIN

		EXEC	dbo.BatchFileDeleteHard 
				@KeyId		=	@KeyId, 
				@KeyType	=	'BatchFileStatusId',
				@AuditId	=	@AuditId

		DELETE	 dbo.BatchFileStatus
		WHERE	 BatchFileStatusId = @KeyId


	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
