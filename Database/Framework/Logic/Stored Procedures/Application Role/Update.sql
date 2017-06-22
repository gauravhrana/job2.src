IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleUpdate')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleUpdate'
	DROP  Procedure  ApplicationRoleUpdate
END
GO

PRINT 'Creating Procedure ApplicationRoleUpdate'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRoleUpdate
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

CREATE Procedure dbo.ApplicationRoleUpdate
(
		@ApplicationRoleId		INT		 			
	,	@Name					VARCHAR(50)				
	,	@Description			VARCHAR(50)			
	,	@SortOrder				INT
	,	@ApplicationId			INT				
	,	@AuditId				INT					
	,	@AuditDate				DATETIME	= NULL	
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRole'	
	
)
AS
BEGIN

	UPDATE	dbo.ApplicationRole 
	SET		ApplicationId		=	@ApplicationId
		,	Name				=	@Name				
		,	Description			=	@Description				
		,	SortOrder			=	@SortOrder	
	WHERE	ApplicationRoleId	=	@ApplicationRoleId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ApplicationRoleId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
 END
GO