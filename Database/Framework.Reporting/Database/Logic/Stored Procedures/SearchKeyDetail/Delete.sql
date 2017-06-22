IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailDelete')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailDelete'
	DROP  Procedure SearchKeyDetailDelete
END
GO

PRINT 'Creating Procedure SearchKeyDetailDelete'
GO
/******************************************************************************
**		File: 
**		Name: SearchKeyDetailDelete
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
CREATE Procedure dbo.SearchKeyDetailDelete
(
		@SearchKeyDetailId 		INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKeyDetail'
)
AS
BEGIN

	DELETE	 dbo.SearchKeyDetail
	WHERE	 SearchKeyDetailId = @SearchKeyDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKeyDetail'
		,	@EntityKey				= @SearchKeyDetailId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
