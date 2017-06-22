IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBComponentNameDelete')
BEGIN
	PRINT 'Dropping Procedure DBComponentNameDelete'
	DROP  Procedure DBComponentNameDelete
END
GO

PRINT 'Creating Procedure DBComponentNameDelete'
GO
/******************************************************************************
**		File: 
**		Name: DBComponentNameDelete
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
CREATE Procedure dbo.DBComponentNameDelete
(
		@DBComponentNameId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'DBComponentName'
)
AS
BEGIN

	DELETE	 dbo.DBComponentName
	WHERE	 DBComponentNameId = @DBComponentNameId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBComponentName'
		,	@EntityKey				= @DBComponentNameId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
