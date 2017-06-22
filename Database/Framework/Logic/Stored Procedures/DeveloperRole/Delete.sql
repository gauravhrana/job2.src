IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DeveloperRoleDelete')
BEGIN
	PRINT 'Dropping Procedure DeveloperRoleDelete'
	DROP  Procedure DeveloperRoleDelete
END
GO

PRINT 'Creating Procedure DeveloperRoleDelete'
GO
/******************************************************************************
**		File: 
**		Name: DeveloperRoleDelete
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
CREATE Procedure dbo.DeveloperRoleDelete
(
		@DeveloperRoleId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'DeveloperRole'
)
AS
BEGIN

	DELETE	 dbo.DeveloperRole
	WHERE	 DeveloperRoleId = @DeveloperRoleId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DeveloperRole'
		,	@EntityKey				= @DeveloperRoleId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
