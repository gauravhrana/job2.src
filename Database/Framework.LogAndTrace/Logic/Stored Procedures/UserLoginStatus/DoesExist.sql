IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserLoginStatusDoesExist')
BEGIN
	PRINT 'Dropping Procedure UserLoginStatusDoesExist'
	DROP  Procedure  UserLoginStatusDoesExist
END
GO

PRINT 'Creating Procedure UserLoginStatusDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: UserLoginStatusDoesExist
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
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.UserLoginStatusDoesExist
(
		@UserLoginStatusId			INT				= NULL
	,	@Name						VARCHAR(50)		
	,	@ApplicationId				INT	
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'UserLoginStatus'
)
AS
BEGIN

	SELECT	a.*
	FROM	dbo.UserLoginStatus a
	WHERE	a.Name			=	@Name
	AND		a.ApplicationId	=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLoginStatus'
		,	@EntityKey				= @UserLoginStatusId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO

