IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'QuickPaginationRunUpdate')
BEGIN
	PRINT 'Dropping Procedure QuickPaginationRunUpdate'
	DROP  Procedure  QuickPaginationRunUpdate
END
GO

PRINT 'Creating Procedure QuickPaginationRunUpdate'
GO

/******************************************************************************
**		File: 
**		Name: QuickPaginationRunUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.QuickPaginationRunUpdate
(
		@QuickPaginationRunId		INT
	,	@ApplicationUserId			INT			
	,	@SystemEntityTypeId			INT	
	,	@SortClause					VARCHAR(50)						
	,	@WhereClause				VARCHAR(500)		
	,	@ExpirationTime				DECIMAL(15,0)				
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'QuickPaginationRun'
)
AS
BEGIN

	UPDATE	dbo.QuickPaginationRun 
	SET		SortClause				=	@SortClause				
		,	WhereClause				=	@WhereClause				
		,	ApplicationUserId		=	@ApplicationUserId
		,	SystemEntityTypeId		=	@SystemEntityTypeId
		,	ExpirationTime			=	@ExpirationTime							
	WHERE	QuickPaginationRunId	=	@QuickPaginationRunId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'QuickPaginationRun'
		,	@EntityKey				= @QuickPaginationRunId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

 END		
 GO