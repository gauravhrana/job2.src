IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'QuickPaginationRunDelete')
BEGIN
	PRINT 'Dropping Procedure QuickPaginationRunDelete'
	DROP  Procedure QuickPaginationRunDelete
END
GO

PRINT 'Creating Procedure QuickPaginationRunDelete'
GO
/******************************************************************************
**		File: 
**		Name: QuickPaginationRunDelete
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
**		Date:		Author:				WhereClause:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.QuickPaginationRunDelete
(
		@QuickPaginationRunId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'QuickPaginationRun'
)
AS
BEGIN

	DELETE	 dbo.QuickPaginationRun
	WHERE	 QuickPaginationRunId = @QuickPaginationRunId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'QuickPaginationRun'
		,	@EntityKey				= @QuickPaginationRunId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
