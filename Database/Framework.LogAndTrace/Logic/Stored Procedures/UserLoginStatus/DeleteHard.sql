IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginStatusDeleteHard')
BEGIN
	PRINT 'Dropping Procedure UserLoginStatusDeleteHard'
	DROP  Procedure UserLoginStatusDeleteHard
END
GO

PRINT 'Creating Procedure UserLoginStatusDeleteHard'
GO
/******************************************************************************
**		Task: 
**		Name: UserLoginStatusDelete
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
CREATE Procedure dbo.UserLoginStatusDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'UserLoginStatus'
)
AS
BEGIN
	IF @KeyType = 'UserLoginStatusId'
	BEGIN

		DELETE	 dbo.UserLoginStatus
		WHERE	 UserLoginStatusId = @KeyId

	END

	-- Create Audit Record	-- NOT APPLICABLE DATA IS GONE
	
END
GO
