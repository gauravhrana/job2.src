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
CREATE procedure [dbo].[ReleaseNotesSearch]
(
		@ApplicationId				INT 			= NULL
	,	@ReleaseIssueTypeId			INT				= NULL
	,	@ReleasePublishCategoryId	INT				= NULL 
	,	@Description				VARCHAR(MAX)	= NULL 
	,	@PrimaryDeveloper			VARCHAR(MAX)	= NULL
	,	@JIRA						VARCHAR(MAX)	= NULL
	,	@ModuleId					INT				= NULL
	,	@ReleaseFeatureId			INT				= NULL
	,	@Feature					VARCHAR(MAX)	= NULL
	,	@PrimaryEntity				VARCHAR(MAX)	= NULL
	,	@SystemEntityTypeId			INT				= NULL
	,	@PublishType				VARCHAR(MAX)	= NULL	
	,	@ReleaseDateMin				DATETIME		= NULL	
	,	@ReleaseDateMax				DATETIME		= NULL
	,	@UpdatedDateRangeMin		DATETIME		= NULL 
	,	@UpdatedDateRangeMax		DATETIME		= NULL 
	,	@ReleaseLogId				INT				= NULL
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL	
	,	@SystemEntityType			VARCHAR(50)		= 'ReleaseLog'	 
)
AS
BEGIN

SET ANSI_NULLS ON;		

	-- assume search on all possiblities ('%')
	SET	@Description		= ISNULL(@Description,'%')
	SET	@PrimaryDeveloper	= ISNULL(@PrimaryDeveloper,'%')
	SET	@JIRA				= ISNULL(@JIRA,'%')
	SET	@Feature			= ISNULL(@Feature,'%')
	SET	@PrimaryEntity		= ISNULL(@PrimaryEntity,'%')
	SET	@PublishType		= ISNULL(@PublishType,'%')	
	
	--if blank, then assume search on all possiblities ('%')	
	
	IF LEN(LTRIM(RTRIM(@Description))) = 0 
	BEGIN
		SET	@Description = '%'
	END 
	
	IF LEN(LTRIM(RTRIM(@JIRA))) = 0 
	BEGIN
		SET	@JIRA = '%'
	END  	
	
	IF LEN(LTRIM(RTRIM(@Feature))) = 0 
	BEGIN
		SET	@Feature = '%'
	END  	
	
	IF LEN(LTRIM(RTRIM(@PrimaryDeveloper))) = 0 
	BEGIN
		SET	@PrimaryDeveloper = '%'
	END  
	
	IF LEN(LTRIM(RTRIM(@PrimaryEntity))) = 0 
	BEGIN
		SET	@PrimaryEntity = '%'
	END
	
	IF (@UpdatedDateRangeMin IS NULL OR @UpdatedDateRangeMin = '')
	BEGIN
  
	
	SELECT	DISTINCT b.*,	b.Name AS 'ReleaseLog'
	FROM		dbo.ReleaseLogDetail		a
	RIGHT outer JOIN	dbo.ReleaseLog				b 
		ON	a.ReleaseLogId = b.ReleaseLogId
	WHERE	b.ReleaseLogId =
			CASE
				WHEN @ReleaseLogId IS NULL THEN b.ReleaseLogId
				ELSE @ReleaseLogId
			END
	AND		(a.Description		LIKE @Description OR a.Description IS NULL)
	AND		(a.PrimaryDeveloper	LIKE @PrimaryDeveloper OR a.PrimaryDeveloper IS NULL)
	AND		(a.JIRA				LIKE @JIRA 	 + '%' OR a.JIRA IS NULL)	
	AND		(a.Feature			LIKE @Feature 	 + '%' OR a.Feature IS NULL)
	--AND		(a.PrimaryEntity	LIKE @PrimaryEntity + '%' OR a.PrimaryEntity IS NULL)
	AND		a.ApplicationId =
			CASE
				WHEN @ApplicationId IS NULL THEN a.ApplicationId
				ELSE @ApplicationId
			END			
	
	AND		((a.ReleaseIssueTypeId =
			CASE
				WHEN @ReleaseIssueTypeId IS NULL THEN a.ReleaseIssueTypeId 
				ELSE @ReleaseIssueTypeId
			END) OR a.ReleaseIssueTypeId IS NULL)
			
	AND		a.ReleasePublishCategoryId =
			CASE
				WHEN @ReleasePublishCategoryId IS NULL THEN a.ReleasePublishCategoryId 
				ELSE @ReleasePublishCategoryId
			END
			
	AND		((a.ModuleId =
			CASE
				WHEN @ModuleId IS NULL THEN a.ModuleId 
				ELSE @ModuleId
			END) OR a.ModuleId IS NULL)
			
	AND		((a.ReleaseFeatureId =
			CASE
				WHEN @ReleaseFeatureId IS NULL THEN a.ReleaseFeatureId 
				ELSE @ReleaseFeatureId
			END) OR a.ReleaseFeatureId IS NULL)
			
	AND		((ISNULL(a.SystemEntityTypeId, -1) =
			CASE
				WHEN @SystemEntityTypeId IS NULL THEN ISNULL(a.SystemEntityTypeId, -1)
				ELSE @SystemEntityTypeId
			END) OR a.SystemEntityTypeId IS NULL)
			
	AND	   b.ReleaseDate >=
			CASE
				WHEN @ReleaseDateMin IS NULL THEN b.ReleaseDate
				ELSE @ReleaseDateMin
			END
			
	AND		b.ReleaseDate <=
			CASE
				WHEN @ReleaseDateMax IS NULL THEN b.ReleaseDate
				ELSE @ReleaseDateMax
			END 
	ORDER BY	b.Name Desc	
	
	SELECT	a.ReleaseLogDetailId
		,	a.ApplicationId	
		,   a.ReleaseLogId              
		,	a.ItemNo                    
		,	a.Description				
		,	a.SortOrder					
		,	a.RequestedBy               
		,	a.PrimaryDeveloper          
		,	a.RequestedDate AS 'ReleaseDate'
		,	a.ReleaseIssueTypeId			
		,	a.ReleasePublishCategoryId	
		,	a.JIRA						
		,	a.Feature				  
		,	a.ModuleId	
		,	a.ReleaseFeatureId					
		,	a.TimeSpent					
		,	a.PrimaryEntity		
		,	a.SystemEntityTypeId				
		,	b.Name							AS	'ReleaseLog'
		,	c.Name							AS	'ReleaseIssueType'		
		,	d.Name							AS	'ReleasePublishCategory'
	  	,	e.Name							AS	'Module' 
	  	,	f.Name 							AS  'Application'		
		,	g.Name							AS  'ReleaseFeature'	
		,	h.EntityName					AS  'SystemEntityType'
	FROM		dbo.ReleaseLogDetail		a
		INNER JOIN	dbo.ReleaseLog				b 
			ON	a.ReleaseLogId = b.ReleaseLogId
		INNER JOIN	dbo.ReleaseIssueType		c
			ON	a.ReleaseIssueTypeId = c.ReleaseIssueTypeId
		INNER JOIN	dbo.ReleasePublishCategory	d
			ON	a.ReleasePublishCategoryId = d.ReleasePublishCategoryId
		INNER JOIN	dbo.Module	e
			ON	a.ModuleId = e.ModuleId
		INNER JOIN AuthenticationAndAuthorization.dbo.Application f 
			ON a.ApplicationId = f.ApplicationId	
		INNER JOIN dbo.ReleaseFeature g
			ON a.ReleaseFeatureId = g.ReleaseFeatureId	
	   LEFT JOIN Configuration.dbo.SystemEntityType h 
			ON a.SystemEntityTypeId = h.SystemEntityTypeId
	WHERE	a.Description		LIKE @Description + '%'
	AND		a.PrimaryDeveloper	LIKE @PrimaryDeveloper + '%'
	AND		a.JIRA				LIKE @JIRA 	 + '%'
	AND		a.Feature			LIKE @Feature 	 + '%'
	--AND		a.PrimaryEntity		LIKE @PrimaryEntity + '%'
	AND		a.ApplicationId =
			CASE
				WHEN @ApplicationId IS NULL THEN a.ApplicationId
				ELSE @ApplicationId
			END
	AND		b.ReleaseLogId =
			CASE
				WHEN @ReleaseLogId IS NULL THEN b.ReleaseLogId
				ELSE @ReleaseLogId
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
	AND		a.ModuleId =
			CASE
				WHEN @ModuleId IS NULL THEN a.ModuleId 
				ELSE @ModuleId
			END 
	AND		a.ReleaseFeatureId =
			CASE
				WHEN @ReleaseFeatureId IS NULL THEN a.ReleaseFeatureId 
				ELSE @ReleaseFeatureId
			END 
	AND		ISNULL(a.SystemEntityTypeId, -1) =
			CASE
				WHEN @SystemEntityTypeId IS NULL THEN ISNULL(a.SystemEntityTypeId, -1) 
				ELSE @SystemEntityTypeId
			END
	AND		a.RequestedDate >=
			CASE
				WHEN @ReleaseDateMin IS NULL THEN a.RequestedDate
				ELSE @ReleaseDateMin
			END
	AND		a.RequestedDate <=
			CASE
				WHEN @ReleaseDateMax IS NULL THEN a.RequestedDate
				ELSE @ReleaseDateMax
			END
	ORDER BY	b.SortOrder	
	
	
  END
  ELSE
  
  BEGIN 
  
	SELECT	Distinct	a.EntityKey   
	INTO	#TempDatelist
	FROM	CommonServices.dbo.AuditHistory a
	WHERE	(CONVERT(VARCHAR(10),CreatedDate ,101) >= CONVERT(VARCHAR(10),@UpdatedDateRangeMin ,101) 
	AND 	CONVERT(VARCHAR(10),CreatedDAte ,101) <= CONVERT(VARCHAR(10),@UpdatedDateRangeMax ,101))
	AND		a.SystemEntityId		 = 4100
	AND		a.AuditActionId IN (1,2)
	ORDER BY a.EntityKey  							ASC
  
  SELECT	DISTINCT b.*, b.Name AS 'ReleaseLog'
  ,			(
				CASE 
					WHEN c.EntityKey IS NOT NULL AND c.EntityKey = a.ReleaseLogDetailId  
						THEN 1 
					ELSE 0 
				END
			) AS 'IsUpdated'      
	FROM		dbo.ReleaseLogDetail		a
	INNER  JOIN	dbo.ReleaseLog				b 
		ON	a.ReleaseLogId = b.ReleaseLogId
	Left Outer JOIN #TempDatelist c 
		ON c.EntityKey = a.ReleaseLogDetailId 
	WHERE	a.Description		LIKE @Description + '%'
	AND		a.PrimaryDeveloper	LIKE @PrimaryDeveloper + '%'
	AND		a.JIRA				LIKE @JIRA 	 + '%'
	AND		a.Feature			LIKE @Feature 	 + '%'
	--AND		a.PrimaryEntity		LIKE @PrimaryEntity + '%'
	AND		a.ApplicationId =
			CASE
				WHEN @ApplicationId IS NULL THEN a.ApplicationId
				ELSE @ApplicationId
			END
	AND		b.ReleaseLogId =
			CASE
				WHEN @ReleaseLogId IS NULL THEN b.ReleaseLogId
				ELSE @ReleaseLogId
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
	AND		a.ModuleId =
			CASE
				WHEN @ModuleId IS NULL THEN a.ModuleId 
				ELSE @ModuleId
			END
	AND		a.ReleaseFeatureId =
			CASE
				WHEN @ReleaseFeatureId IS NULL THEN a.ReleaseFeatureId 
				ELSE @ReleaseFeatureId
			END 
	AND		ISNULL(a.SystemEntityTypeId, -1) =
			CASE
				WHEN @SystemEntityTypeId IS NULL THEN ISNULL(a.SystemEntityTypeId, -1) 
				ELSE @SystemEntityTypeId
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
	ORDER BY	b.Name Desc	

   
	SELECT	a.ReleaseLogDetailId
		,	a.ApplicationId	
	    ,   a.ReleaseLogId              
		,	a.ItemNo                    
		,	a.Description				
		,	a.SortOrder					
		,	a.RequestedBy               
		,	a.PrimaryDeveloper          
		,	a.RequestedDate AS 'ReleaseDate'
		,	a.ReleaseIssueTypeId			
		,	a.ReleasePublishCategoryId	
		,	a.JIRA		   
		,	a.ModuleId	
		,	a.ReleaseFeatureId									
		,	a.TimeSpent						
		,	a.Feature					
		,	a.PrimaryEntity		
		,	a.SystemEntityTypeId				
		,	b.Name							AS	'ReleaseLog'
		,	c.Name							AS	'ReleaseIssueType'		
		,	d.Name							AS	'ReleasePublishCategory'
		,	e.Name							AS	'Module' 
		,	f.Name 							AS  'Application'	
		,	g.Name							AS	'ReleaseFeature'
		,	h.EntityName					AS 'SystemEntityType'
	FROM		dbo.ReleaseLogDetail		a
		INNER JOIN	dbo.ReleaseLog				b 
			ON	a.ReleaseLogId = b.ReleaseLogId
		INNER JOIN	dbo.ReleaseIssueType		c
			ON	a.ReleaseIssueTypeId = c.ReleaseIssueTypeId
		INNER JOIN	dbo.ReleasePublishCategory	d
			ON	a.ReleasePublishCategoryId = d.ReleasePublishCategoryId
		 INNER JOIN	dbo.Module	e
			ON	a.ModuleId = e.ModuleId
		INNER JOIN AuthenticationAndAuthorization.dbo.Application f 
			ON a.ApplicationId = f.ApplicationId
		INNER JOIN	dbo.ReleaseFeature g
			ON a.ReleaseFeatureId = g.ReleaseFeatureId
	   INNER JOIN #TempDatelist 
			ON 	#TempDatelist.EntityKey=a.ReleaseLogDetailId
	   LEFT JOIN Configuration.dbo.SystemEntityType h 
			ON a.SystemEntityTypeId = h.SystemEntityTypeId
	WHERE	a.Description		LIKE @Description + '%'
	AND		a.PrimaryDeveloper	LIKE @PrimaryDeveloper + '%'
	AND		a.JIRA				LIKE @JIRA 	 + '%'
	AND		a.Feature			LIKE @Feature 	 + '%'	
	--AND		a.PrimaryEntity		LIKE @PrimaryEntity + '%'
	AND		a.ApplicationId =
			CASE
				WHEN @ApplicationId IS NULL THEN a.ApplicationId
				ELSE @ApplicationId
			END
	AND		b.ReleaseLogId =
			CASE
				WHEN @ReleaseLogId IS NULL THEN b.ReleaseLogId
				ELSE @ReleaseLogId
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
	AND		a.ModuleId =
			CASE
				WHEN @ModuleId IS NULL THEN a.ModuleId 
				ELSE @ModuleId
			END
	AND		a.ReleaseFeatureId =
			CASE
				WHEN @ReleaseFeatureId IS NULL THEN a.ReleaseFeatureId 
				ELSE @ReleaseFeatureId
			END 
	AND		ISNULL(a.SystemEntityTypeId, -1) =
			CASE
				WHEN @SystemEntityTypeId IS NULL THEN ISNULL(a.SystemEntityTypeId, -1) 
				ELSE @SystemEntityTypeId
			END
	AND		a.RequestedDate >=
			CASE
				WHEN @ReleaseDateMin IS NULL THEN b.ReleaseDate
				ELSE @ReleaseDateMin
			END
	AND		a.RequestedDate <=
			CASE
				WHEN @ReleaseDateMax IS NULL THEN a.RequestedDate
				ELSE @ReleaseDateMax
			END
	ORDER BY	b.SortOrder
   DROP TABLE #TempDatelist	
   
 END
 	
	
	
	-- Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO



