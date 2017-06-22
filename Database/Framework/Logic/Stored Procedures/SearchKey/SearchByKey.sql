IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SearchKeySearchByKey')
BEGIN
	PRINT 'Dropping Procedure SearchKeySearchByKey'
	DROP Procedure SearchKeySearchByKey
END
GO

PRINT 'Creating Procedure SearchKeySearchByKey'
GO

/******************************************************************************
**		File: 
**		Name: SearchKeySearchByKey
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**
**		Sample:   
**              
			EXEC SearchKeySearchByKey NULL	, NULL	, NULL
			EXEC SearchKeySearchByKey NULL	, 'K'	, NULL
			EXEC SearchKeySearchByKey 1		, 'K'	, NULL
			EXEC SearchKeySearchByKey 1		, NULL	, NULL
			EXEC SearchKeySearchByKey NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------	--------			-------------------------------------------
**    
*******************************************************************************/
Create procedure SearchKeySearchByKey
(
		@SearchKeyId					INT
	,	@ApplicationId				INT				= NULL
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'SearchKey'
)
AS
BEGIN

	
	SELECT	* 
	FROM	dbo.SearchKey	a 
	WHERE	a.SearchKeyId = @SearchKeyId
	AND		a.ApplicationId		  = ISNULL(@ApplicationId, a.ApplicationId )

	SELECT	* 
	FROM	dbo.SearchKeyDetail		a
	WHERE	a.SearchKeyId = @SearchKeyId
	AND		a.ApplicationId		  = ISNULL(@ApplicationId, a.ApplicationId )

	SELECT	* 
	FROM	dbo.SearchKeyDetailItem	a
	WHERE	a.SearchKeyDetailId IN ( 
				SELECT	SearchKeyDetailId 
				FROM	dbo.SearchKeyDetail 
				WHERE	SearchKeyId = @SearchKeyId
			)
	AND		a.ApplicationId		  = ISNULL(@ApplicationId, a.ApplicationId )

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SearchKey'
		,	@EntityKey				= @SearchKeyId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	

END
GO
	

