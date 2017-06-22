IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDelete')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDelete'
	DROP  Procedure SearchKeyDelete
END
GO

PRINT 'Creating Procedure SearchKeyDelete'
GO
/******************************************************************************
**		File: 
**		Name: SearchKeyDelete
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
CREATE Procedure dbo.SearchKeyDelete
(
		@SearchKeyId 			INT						
	,	@AuditId				INT						
	,	@AuditDate				DATETIME	= NULL		
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKey'
)
AS
BEGIN

	DELETE	 dbo.SearchKey
	WHERE	 SearchKeyId = @SearchKeyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKey'
		,	@EntityKey				= @SearchKeyId
		,	@AuditAction			= 'Delete' 
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END
GO
