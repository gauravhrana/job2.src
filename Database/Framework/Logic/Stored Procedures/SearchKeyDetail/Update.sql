IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailUpdate')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailUpdate'
	DROP  Procedure  SearchKeyDetailUpdate
END
GO

PRINT 'Creating Procedure SearchKeyDetailUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeyDetailUpdate
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
**		--------	--------		-------------------------------------------
**    
*******************************************************************************/

CREATE Procedure dbo.SearchKeyDetailUpdate
(
		@SearchKeyDetailId			INT	
	,	@ApplicationId				INT		
	,	@SearchParameter			VARCHAR(200)		
	,	@SearchKeyId				INT		
	,	@SortOrder					INT								
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKeyDetail'
)
AS
BEGIN

	UPDATE	dbo.SearchKeyDetail 
	SET		ApplicationId			= 	@ApplicationId	
		,	SearchKeyId				=	@SearchKeyId
		,	SearchParameter			=	@SearchParameter		
		,	SortOrder				=	@SortOrder												
	WHERE	SearchKeyDetailId		=	@SearchKeyDetailId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKeyDetail'
		,	@EntityKey				= @SearchKeyDetailId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

 END		
 GO