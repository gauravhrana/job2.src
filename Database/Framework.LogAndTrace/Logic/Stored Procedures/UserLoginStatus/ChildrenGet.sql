IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'UserLoginStatusChildrenGet')
BEGIN
	PRINT 'Dropping Procedure UserLoginStatusChildrenGet'
	DROP  Procedure UserLoginStatusChildrenGet
END
GO

PRINT 'Creating Procedure UserLoginStatusChildrenGet'
GO


/******************************************************************************
**		File: 
**		Name: UserLoginStatusChildrenGet
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

CREATE Procedure dbo.UserLoginStatusChildrenGet
(
		@UserLoginStatusId		INT					
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL   
	,	@SystemEntityType		VARCHAR(50) = 'UserLoginStatus'
)
AS
BEGIN

	-- GET UserLogin Records
	SELECT	a.UserLoginId			
		,	a.ApplicationId
		,	a.UserName						
		,	a.RecordDate			
		,	a.UserLoginStatusId		
		,	b.Name					AS	'UserLoginStatus'		
	FROM		dbo.UserLogin		a
	INNER JOIN	dbo.UserLoginStatus	b	ON	a.UserLoginStatusId	=	b.UserLoginStatusId
	WHERE	a.UserLoginStatusId = @UserLoginStatusId
	
	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @UserLoginStatusId
		,	@AuditAction			= 'ChildrenGet'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END	
GO
   