IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleClone')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleClone'
	DROP  Procedure ApplicationUserXApplicationRoleClone
END
GO

PRINT 'Creating Procedure ApplicationUserXApplicationRoleClone'
GO

/*********************************************************************************************
**		File: 
**		Name: ApplicationUserXApplicationRoleClone
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

CREATE Procedure dbo.ApplicationUserXApplicationRoleClone
(
		@ApplicationUserXApplicationRoleId		INT			= NULL	
	,	@ApplicationId							INT			= NULL	
	,	@ApplicationUserId						INT			= NULL	
	,	@ApplicationRoleId						INT			= NULL	
	,	@AuditId								INT			= NULL					
	,	@AuditDate								DATETIME	= NULL
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationUserXApplicationRole'
)
AS
BEGIN		
	
	SELECT	@ApplicationId			= ApplicationId
		,	@ApplicationUserId		= ApplicationUserId
		,	@ApplicationRoleId		= ApplicationRoleId				
	FROM	dbo.ApplicationUserXApplicationRole
	WHERE	ApplicationUserXApplicationRoleId	= @ApplicationUserXApplicationRoleId
	ORDER BY ApplicationUserXApplicationRoleId

	EXEC dbo.ApplicationUserXApplicationRoleInsert 
			@ApplicationUserXApplicationRoleId		=	NULL
		,	@ApplicationId 					     	=	@ApplicationId
		,	@ApplicationUserId						=	@ApplicationUserId
		,	@ApplicationRoleId						=	@ApplicationRoleId
		,	@AuditId								=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationUserXApplicationRoleId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
