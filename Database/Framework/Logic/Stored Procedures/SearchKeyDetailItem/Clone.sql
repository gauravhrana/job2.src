IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailItemClone')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailItemClone'
	DROP  Procedure SearchKeyDetailItemClone
END
GO

PRINT 'Creating Procedure SearchKeyDetailItemClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SearchKeyDetailItemClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				[Value]:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.SearchKeyDetailItemClone
(
		@SearchKeyDetailItemId		INT			= NULL 	OUTPUT		
	,	@ApplicationId				INT			= NULL
	,	@SearchKeyDetailId			INT						
	,	@Value						VARCHAR(100)						
	,	@SortOrder					INT								
	,	@AuditId					INT									
	,	@AuditDate					DATETIME	= NULL				
	,	@SystemEntityType			VARCHAR(50)	= 'SearchKeyDetailItem'
)
AS
BEGIN
	
	SELECT	@ApplicationId		= ApplicationId
		,	@Value				= [Value]
		,	@SortOrder			= SortOrder				
	FROM	dbo.SearchKeyDetailItem
	WHERE	SearchKeyDetailItemId	= @SearchKeyDetailItemId
	ORDER BY SearchKeyDetailItemId

	EXEC dbo.SearchKeyDetailItemInsert 
			@SearchKeyDetailItemId	=	NULL
		,	@ApplicationId			=   @ApplicationId
		,	@SearchKeyDetailId		=	@SearchKeyDetailId
		,	@Value					=	@Value
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SearchKeyDetailItemId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
