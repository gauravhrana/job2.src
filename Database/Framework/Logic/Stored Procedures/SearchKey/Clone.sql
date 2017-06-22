IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'SearchKeyClone')
BEGIN
	PRINT 'Dropping Procedure SearchKeyClone'
	DROP  Procedure SearchKeyClone
END
GO

PRINT 'Creating Procedure SearchKeyClone'
GO

/*********************************************************************************************
**		File: 
**		Name: SearchKeyClone
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

CREATE Procedure dbo.SearchKeyClone
(
		@SearchKeyId			INT			= NULL 	OUTPUT	
	,   @ApplicationId			INT			= NULL	
	,	@Name					VARCHAR(50)
	,	@View					VARCHAR(100)
	,	@Description			VARCHAR(50)						
	,	@SortOrder				INT							
	,	@AuditId				INT									
	,	@AuditDate				DATETIME	= NULL				
	,	@SystemEntityType		VARCHAR(50)	= 'SearchKey'
)
AS
BEGIN					
	
	SELECT	@ApplicationId			= ApplicationId
		,	@Description			= Description
		,	@View					=[View]
		,	@SortOrder				= SortOrder					
	FROM	dbo.SearchKey
	WHERE   SearchKeyId				= @SearchKeyId
	ORDER BY SearchKeyId

	EXEC dbo.SearchKeyInsert 
			@SearchKeyId			=	NULL
		,   @ApplicationId			=   ApplicationId
		,	@Name					=	@Name
		,	@Description			=	@Description
		,	@SortOrder				=	@SortOrder
		,	@View					=	@View		
		,	@AuditId				=	@AuditId

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKey'
		,	@EntityKey				= @SearchKeyId
		,	@AuditAction			= 'Clone'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	

END	
GO
