IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailItemUpdate')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailItemUpdate'
	DROP  Procedure  SearchKeyDetailItemUpdate
END
GO

PRINT 'Creating Procedure SearchKeyDetailItemUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeyDetailItemUpdate
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
**		Date:		Author:				Value:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SearchKeyDetailItemUpdate
(
		@SearchKeyDetailItemId		INT		
	,	@SearchKeyDetailId			INT					
	,	@Value						VARCHAR (200)	
	,	@SortOrder					INT						
	,	@AuditId					INT							
	,	@AuditDate					DATETIME	= NULL		
	,	@SystemEntityType			VARCHAR(50)	= 'SearchKeyDetailItem'
)
AS
BEGIN

	UPDATE	dbo.SearchKeyDetailItem 
	SET		SearchKeyDetail		=   @SearchKeyDetail					
		,	Value				=	@Value				
		,	SortOrder			=	@SortOrder							
	WHERE	SearchKeyDetailItemId		=	@SearchKeyDetailItemId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType	
		,	@EntityKey				= @SearchKeyDetailItemId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
END		
GO