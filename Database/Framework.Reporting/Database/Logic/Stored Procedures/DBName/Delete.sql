IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'DBNameDelete')
BEGIN
	PRINT 'Dropping Procedure DBNameDelete'
	DROP  Procedure DBNameDelete
END
GO

PRINT 'Creating Procedure DBNameDelete'
GO
/******************************************************************************
**		File: 
**		Name: DBNameDelete
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
CREATE Procedure dbo.DBNameDelete
(
		@DBNameId 			INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'DBName'
)
AS
BEGIN

	DELETE	 dbo.DBName
	WHERE	 DBNameId = @DBNameId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'DBName'
		,	@EntityKey				= @DBNameId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
