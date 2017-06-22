IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleClone'
	DROP  Procedure ApplicationOperationXApplicationRoleClone
END
GO

PRINT 'Creating Procedure ApplicationOperationXApplicationRoleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationOperationXApplicationRoleClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.ApplicationOperationXApplicationRoleClone
(
		@ApplicationOperationXApplicationRoleId		INT			= NULL	
	,	@ApplicationId								INT			= NULL
	,	@ApplicationOperationId						INT			= NULL	
	,	@ApplicationRoleId							INT			= NULL	
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL
	,	@SystemEntityType							VARCHAR(50)	= 'ApplicationOperationXApplicationRole'
)
AS
BEGIN		
	
	SELECT	@ApplicationId				= ApplicationId
		,	@ApplicationOperationId		= ApplicationOperationId
		,	@ApplicationRoleId			= ApplicationRoleId				
	FROM	dbo.ApplicationOperationXApplicationRole
	WHERE	ApplicationOperationXApplicationRoleId	= @ApplicationOperationXApplicationRoleId
	ORDER BY ApplicationOperationXApplicationRoleId

	EXEC dbo.ApplicationOperationXApplicationRoleInsert 
			@ApplicationOperationXApplicationRoleId		=	NULL
		,	@ApplicationId								=	@ApplicationId
		,	@ApplicationOperationId						=	@ApplicationOperationId
		,	@ApplicationRoleId							=	@ApplicationRoleId
		,	@AuditId									=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationOperationXApplicationRoleId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
