IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailItemDelete')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailItemDelete'
	DROP  Procedure SearchKeyDetailItemDelete
END
GO

PRINT 'Creating Procedure SearchKeyDetailItemDelete'
GO
/******************************************************************************
**		File: 
**		Name: SearchKeyDetailItemDelete
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
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.SearchKeyDetailItemDelete
(
		@SearchKeyDetailItemId 		INT						
	,	@AuditId					INT						
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'SearchKeyDetailItem'
)
AS
BEGIN

	DELETE	 dbo.SearchKeyDetailItem
	WHERE	 SearchKeyDetailItemId = @SearchKeyDetailItemId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SearchKeyDetailItemId
		,	@AuditAction			= 'Delete'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END
GO
