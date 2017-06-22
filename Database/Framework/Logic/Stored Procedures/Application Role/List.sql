IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'ApplicationRoleList')
BEGIN
	PRINT 'Dropping Procedure ApplicationRoleList'
	DROP  Procedure  dbo.ApplicationRoleList
END
GO

PRINT 'Creating Procedure ApplicationRoleList'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationRoleList
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
**     ----------					   ---------
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

CREATE Procedure dbo.ApplicationRoleList
(
		@AuditId				INT				
	,	@ApplicationId			INT				
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'ApplicationRole'
)
AS
BEGIN
	
	SELECT		ApplicationRoleId	
			,	Name				
			,	Description	
			,	SortOrder
			,	ApplicationId			
	FROM	dbo.ApplicationRole 	
	WHERE	ApplicationId		=	@ApplicationId
	ORDER BY SortOrder						ASC	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'List'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO