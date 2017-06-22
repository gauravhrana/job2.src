IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleDelete'
	DROP  Procedure ApplicationOperationXApplicationRoleDelete
END
GO

PRINT 'Creating Procedure ApplicationOperationXApplicationRoleDelete'
GO
/******************************************************************************
**		File: 
**		Name: ApplicationOperationXApplicationRoleDelete
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
CREATE Procedure dbo.ApplicationOperationXApplicationRoleDelete
(
		@ApplicationOperationXApplicationRoleId		INT			= NULL	
	,	@ApplicationOperationId						INT			= NULL	
	,	@ApplicationRoleId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'ApplicationOperationXApplicationRole'
)
AS
BEGIN

	DELETE	dbo.ApplicationOperationXApplicationRole
	WHERE	ApplicationOperationXApplicationRoleId	=	ISNULL(@ApplicationOperationXApplicationRoleId,	ApplicationOperationXApplicationRoleId)	
	AND		ApplicationOperationId					=	ISNULL(@ApplicationOperationId,			ApplicationOperationId)
	AND		ApplicationRoleId						=	ISNULL(@ApplicationRoleId,			ApplicationRoleId)

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationOperationXApplicationRoleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
