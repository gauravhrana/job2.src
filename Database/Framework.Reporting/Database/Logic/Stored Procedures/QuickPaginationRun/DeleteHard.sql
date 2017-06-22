IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'QuickPaginationRunDeleteHard')
BEGIN
	PRINT 'Dropping Procedure QuickPaginationRunDeleteHard'
	DROP  Procedure QuickPaginationRunDeleteHard
END
GO

PRINT 'Creating Procedure QuickPaginationRunDeleteHard'
GO
/******************************************************************************
**		Task: 
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
**     ----------							-----------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				WhereClause:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.QuickPaginationRunDeleteHard
(
		@KeyId 					INT						
	,	@KeyType				VARCHAR(50)				
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL			
	,	@SystemEntityType		VARCHAR(50)	= 'QuickPaginationRun'
)
AS
BEGIN

	IF @KeyType = 'QuickPaginationRunId'
	BEGIN

		DELETE	 dbo.QuickPaginationRun
		WHERE	 QuickPaginationRunId = @KeyId
	END

	
END
GO
