IF EXISTS(SELECT * FROM sysobjects WHERE type='P' AND name='UserLoginDoesExist')
BEGIN
	PRINT 'Dropping Procedure UserLoginDoesExist'
	DROP  Procedure  UserLoginDoesExist
END
GO

PRINT 'Creating Procedure UserLoginDoesExist'
GO

/******************************************************************************
**		File: 
**		Name: UserLoginDoesExist
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
**		Date:		Author:				RecordDate:
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

Create procedure dbo.UserLoginDoesExist
(
		@UserLoginId				INT				= NULL
	,	@UserName					VARCHAR(50)		
	,	@ApplicationId				INT	
	,	@AuditId					INT							
	,	@AuditDate					DATETIME		= NULL		
	,	@SystemEntityType			VARCHAR(50)		= 'UserLogin'
)
AS
BEGIN

	SELECT	a.*
	FROM	dbo.UserLogin a
	WHERE	a.UserName			=	@UserName
	AND		a.ApplicationId	=	@ApplicationId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'UserLogin'
		,	@EntityKey				= @UserLoginId
		,	@AuditAction			= 'DoesExist'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO

