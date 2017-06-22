IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleDelete')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleDelete'
	DROP  Procedure ApplicationRoleDelete
END
GO

PRINT 'Creating Procedure ApplicationRoleDelete'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRoleDelete
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
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.ApplicationRoleDelete
(
		@ApplicationRoleId 		INT				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRole'	
)
AS
BEGIN
	
	DELETE	 dbo.ApplicationRole
	WHERE	 ApplicationRoleId	= @ApplicationRoleId

	--Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @ApplicationRoleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
