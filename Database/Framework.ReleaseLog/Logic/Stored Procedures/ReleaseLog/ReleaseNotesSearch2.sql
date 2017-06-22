IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ReleaseNotesSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseNotesSearch'
	DROP Procedure ReleaseNotesSearch
END
GO

PRINT 'Creating Procedure ReleaseNotesSearch'
GO

/******************************************************************************
**		File: 
**		Name: ReleaseNotesSearch
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
			EXEC ReleaseNotesSearch NULL	, NULL	, NULL
			EXEC ReleaseNotesSearch NULL	, 'K'	, NULL
			EXEC ReleaseNotesSearch 1		, 'K'	, NULL
			EXEC ReleaseNotesSearch 1		, NULL	, NULL
			EXEC ReleaseNotesSearch NULL	, NULL	, 'W'

**		Parameters:
**		Input							Output
**      ----------						-----------
**
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
Create procedure dbo.ReleaseNotesSearch
(
		@ApplicationId				INT 			= NULL
	,	@ReleaseIssueTypeId			INT				= NULL
	,	@ReleasePublishCategoryId	INT				= NULL
	,	@PrimaryDeveloper			VARCHAR(MAX)	= NULL
	,	@JIRA						VARCHAR(MAX)	= NULL
	,	@Feature					VARCHAR(MAX)	= NULL
	,	@PrimaryEntity				VARCHAR(MAX)	= NULL
	,	@PublishType				VARCHAR(MAX)	= NULL	
	,	@ReleaseDateMin				DATETIME		= NULL	
	,	@ReleaseDateMax				DATETIME		= NULL
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ReleaseLog'	 
)
AS
BEGIN

	-- assume search on all possiblities ('%')
	SET	@PrimaryDeveloper = ISNULL(@PrimaryDeveloper,'%')
	SET	@JIRA = ISNULL(@JIRA,'%')
	SET	@Feature = ISNULL(@Feature,'%')
	SET	@PrimaryEntity = ISNULL(@PrimaryEntity,'%')
	SET	@PublishType = ISNULL(@PublishType,'%')
	
	--if blank, then assume search on all possiblities ('%')	
	IF LEN(LTRIM(RTRIM(@PrimaryDeveloper))) = 0 
	BEGIN
		SET	@PrimaryDeveloper = '%'
	END
	
	IF LEN(LTRIM(RTRIM(@JIRA))) = 0 
	BEGIN
		SET	@JIRA = '%'
	END  	
	
	IF LEN(LTRIM(RTRIM(@Feature))) = 0 
	BEGIN
		SET	@Feature = '%'
	END  
	
	IF LEN(LTRIM(RTRIM(@PrimaryEntity))) = 0 
	BEGIN
		SET	@PrimaryEntity = '%'
	END

	SELECT	DISTINCT b.*
	FROM		dbo.ReleaseLogDetail		a
	INNER JOIN	dbo.ReleaseLog				b 
		ON	a.ReleaseLogId = b.ReleaseLogId
	WHERE	a.PrimaryDeveloper	LIKE @PrimaryDeveloper + '%'
	AND		a.JIRA				LIKE @JIRA 	 + '%'
	AND		a.Feature			LIKE @Feature 	 + '%'
	AND		a.PrimaryEntity		LIKE @PrimaryEntity + '%'
	AND		a.ApplicationId =
			CASE
				WHEN @ApplicationId IS NULL THEN a.ApplicationId
				ELSE @ApplicationId
			END
	AND		a.ReleaseIssueTypeId =
			CASE
				WHEN @ReleaseIssueTypeId IS NULL THEN a.ReleaseIssueTypeId 
				ELSE @ReleaseIssueTypeId
			END
	AND		a.ReleasePublishCategoryId =
			CASE
				WHEN @ReleasePublishCategoryId IS NULL THEN a.ReleasePublishCategoryId 
				ELSE @ReleasePublishCategoryId
			END
	AND		b.ReleaseDate >=
			CASE
				WHEN @ReleaseDateMin IS NULL THEN b.ReleaseDate
				ELSE @ReleaseDateMin
			END
	AND		b.ReleaseDate <=
			CASE
				WHEN @ReleaseDateMax IS NULL THEN b.ReleaseDate
				ELSE @ReleaseDateMax
			END
	ORDER BY	b.SortOrder	

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END


GO
	

