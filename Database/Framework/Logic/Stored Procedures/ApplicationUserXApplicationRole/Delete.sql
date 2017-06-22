IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleDelete'
	DROP  Procedure ApplicationUserXApplicationRoleDelete
END
GO

PRINT 'Creating Procedure ApplicationUserXApplicationRoleDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationUserXApplicationRoleDelete
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
CREATE Procedure dbo.ApplicationUserXApplicationRoleDelete
(
		@ApplicationUserXApplicationRoleId		INT			= NULL	
	,	@ApplicationUserId						INT			= NULL	
	,	@ApplicationRoleId							INT			= NULL	
	,	@AuditId									INT			= NULL			
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'ApplicationUserXApplicationRole'
)
AS
BEGIN

	DELETE	dbo.ApplicationUserXApplicationRole
	WHERE	ApplicationUserXApplicationRoleId	=	ISNULL(@ApplicationUserXApplicationRoleId,	ApplicationUserXApplicationRoleId)	
	AND		ApplicationUserId					=	ISNULL(@ApplicationUserId,			ApplicationUserId)
	AND		ApplicationRoleId						=	ISNULL(@ApplicationRoleId,			ApplicationRoleId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserXApplicationRoleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
