IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationUserXApplicationRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationUserXApplicationRoleUpdate'
	DROP  Procedure  ApplicationUserXApplicationRoleUpdate
END
GO

PRINT 'Creating Procedure ApplicationUserXApplicationRoleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationUserXApplicationRoleUpdate
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

CREATE Procedure dbo.ApplicationUserXApplicationRoleUpdate
(
		@ApplicationUserXApplicationRoleId		INT	
	,	@ApplicationId							INT			 			
	,	@ApplicationUserId						INT					
	,	@ApplicationRoleId						INT					
	,	@AuditId								INT			= NULL					
	,	@AuditDate								DATETIME	= NULL	
	,	@SystemEntityType						VARCHAR(50)	= 'ApplicationUserXApplicationRole'
)
AS
BEGIN 

	UPDATE	dbo.ApplicationUserXApplicationRole 
	SET		ApplicationUserId					=	@ApplicationUserId		
		,	ApplicationRoleId					=	@ApplicationRoleId							
	WHERE	ApplicationUserXApplicationRoleId	=	@ApplicationUserXApplicationRoleId
	AND		ApplicationId						=	@ApplicationId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationUserXApplicationRoleId
		,	@AuditAction			= 'Update' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END		
 GO