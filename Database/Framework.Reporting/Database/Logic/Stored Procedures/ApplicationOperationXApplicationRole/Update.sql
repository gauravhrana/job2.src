IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationOperationXApplicationRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationXApplicationRoleUpdate'
	DROP  Procedure  ApplicationOperationXApplicationRoleUpdate
END
GO

PRINT 'Creating Procedure ApplicationOperationXApplicationRoleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationOperationXApplicationRoleUpdate
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

CREATE Procedure dbo.ApplicationOperationXApplicationRoleUpdate
(
		@ApplicationOperationXApplicationRoleId		INT		
	,	@ApplicationId								INT
	,	@ApplicationOperationId						INT					
	,	@ApplicationRoleId							INT					
	,	@AuditId									INT					
	,	@AuditDate									DATETIME	= NULL	
	,	@SystemEntityType							VARCHAR(50)	= 'ApplicationOperationXApplicationRole'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationOperationXApplicationRole 
	SET		ApplicationOperationId		=	@ApplicationOperationId		
		,	ApplicationRoleId			=	@ApplicationRoleId							
	WHERE	ApplicationOperationXApplicationRoleId		=	@ApplicationOperationXApplicationRoleId
	AND		ApplicationId						=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationOperationXApplicationRoleId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO