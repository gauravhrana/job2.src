IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginStatusDelete')
BEGIN
	PRINT 'Dropping Procedure UserLoginStatusDelete'
	DROP  Procedure UserLoginStatusDelete
END
GO

PRINT 'Creating Procedure UserLoginStatusDelete'
GO
/******************************************************************************
**		File: 
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
**     ----------						-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.UserLoginStatusDelete
(
		@UserLoginStatusId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'UserLoginStatus'
)
AS
BEGIN

	DELETE	 dbo.UserLoginStatus
	WHERE	 UserLoginStatusId = @UserLoginStatusId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLoginStatus'
		,	@EntityKey				= @UserLoginStatusId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
