IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBProjectNameDelete')
BEGIN
	PRINT 'Dropping Procedure DBProjectNameDelete'
	DROP  Procedure DBProjectNameDelete
END
GO

PRINT 'Creating Procedure DBProjectNameDelete'
GO
/******************************************************************************
**		File: 
**		Name: DBProjectNameDelete
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
CREATE Procedure dbo.DBProjectNameDelete
(
		@DBProjectNameId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'DBProjectName'
)
AS
BEGIN

	DELETE	 dbo.DBProjectName
	WHERE	 DBProjectNameId = @DBProjectNameId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBProjectName'
		,	@EntityKey				= @DBProjectNameId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
