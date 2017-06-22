IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginDelete')
BEGIN
	PRINT 'Dropping Procedure UserLoginDelete'
	DROP  Procedure UserLoginDelete
END
GO

PRINT 'Creating Procedure UserLoginDelete'
GO
/******************************************************************************
**		File: 
**		Name: UserLoginDelete
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
**		Date:		Author:				RecordDate:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.UserLoginDelete
(
		@UserLoginId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'UserLogin'
)
AS
BEGIN

	DELETE	 dbo.UserLogin
	WHERE	 UserLoginId = @UserLoginId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLogin'
		,	@EntityKey				= @UserLoginId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
