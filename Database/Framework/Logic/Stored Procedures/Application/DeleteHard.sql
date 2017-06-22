IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationDeleteHard'
	DROP  Procedure ApplicationDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: ApplicationDelete
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
CREATE Procedure dbo.ApplicationDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME = NULL			
	,	@SystemEntityType		VARCHAR(50)			= 'Application'	 
)
AS
BEGIN

	IF @KeyType = 'ApplicationId'
	BEGIN

		EXEC	dbo.ApplicationOperationDeleteHard 
				@KeyId		=	@KeyId			 
			,	@KeyType	=	'ApplicationId'	
			,	@AuditId	=	@AuditId

		DELETE	 dbo.Application
		WHERE	 ApplicationId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
