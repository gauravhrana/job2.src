IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleDeleteHard')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleDeleteHard'
	DROP  Procedure ApplicationRoleDeleteHard
END
GO

PRINT 'Creating Procedure ApplicationRoleDeleteHard'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRoleDeleteHard
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
**     ----------						-----------
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
CREATE Procedure dbo.ApplicationRoleDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRole'	
)
AS
BEGIN

	IF @KeyType = 'ApplicationRoleId'
	BEGIN

		EXEC	dbo.ApplicationOperationXApplicationRoleDeleteHard 
				@KeyId		=	@KeyId			 
			,	@KeyType	=	'ApplicationRoleId'	
			,	@AuditId	=	@AuditId

		DELETE	 dbo.ApplicationRole
		WHERE	 ApplicationRoleId = @KeyId

	
	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
