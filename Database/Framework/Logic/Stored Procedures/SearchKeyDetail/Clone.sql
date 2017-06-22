IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyDetailClone')
BEGIN
	PRINT 'Dropping Procedure SearchKeyDetailClone'
	DROP  Procedure SearchKeyDetailClone
END
GO

PRINT 'Creating Procedure SearchKeyDetailClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SearchKeyDetailClone
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
*********************************************************************************************
**		Change History
*********************************************************************************************
**		Date:		Author:				Description:
**		--------	--------			------------------------------------------------------
**		
**********************************************************************************************/

CREATE Procedure dbo.SearchKeyDetailClone
(
		@SearchKeyDetailId		INT				= NULL 	OUTPUT	
	,   @ApplicationId			INT				= NULL		
	,	@SearchParameter		VARCHAR(200)	
	,	@SearchKeyId			INT	
	,	@SortOrder				INT						
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKeyDetail'
)
AS
BEGIN					
	
	IF @SearchKeyDetailId IS NULL OR @SearchKeyDetailId = -999999
	BEGIN
		EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SearchKeyDetailId OUTPUT
	END	

	SELECT	@ApplicationId			= ApplicationId
		,	@SearchParameter		= SearchParameter
		,	@SearchKeyId			= SearchKeyId				
	FROM	dbo.SearchKeyDetail
	WHERE   SearchKeyDetailId		= @SearchKeyDetailId
	ORDER BY SearchKeyDetailId

	EXEC dbo.SearchKeyDetailInsert 
			@SearchKeyDetailId		=	NULL
		,   @ApplicationId			=   @ApplicationId
		,	@SearchParameter		=	@SearchParameter
		,	@SearchKeyId			=	@SearchKeyId
		,	@SortOrder				=	@SortOrder
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKeyDetail'
		,	@EntityKey				= @SearchKeyDetailId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
