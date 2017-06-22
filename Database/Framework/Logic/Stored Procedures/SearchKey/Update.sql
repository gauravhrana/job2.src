IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyUpdate')
BEGIN
	PRINT 'Dropping Procedure SearchKeyUpdate'
	DROP  Procedure  SearchKeyUpdate
END
GO

PRINT 'Creating Procedure SearchKeyUpdate'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeyUpdate
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

CREATE Procedure dbo.SearchKeyUpdate
(
		@SearchKeyId				INT
	,	@Name						VARCHAR(50)						
	,	@Description				VARCHAR(50)						
	,	@SortOrder					INT			
	,	@View						VARCHAR(100)				
	,	@AuditId					INT					
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKey'
)
AS
BEGIN

	UPDATE	dbo.SearchKey 
	SET		Name					=	@Name				
		,	[Description]			=	@Description				
		,	SortOrder				=	@SortOrder
		,	[View]					=	@View					
	WHERE	SearchKeyId				=	@SearchKeyId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKey'
		,	@EntityKey				= @SearchKeyId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

 END		
 GO