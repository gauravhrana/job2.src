IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name ='ReleaseLogDetailSearch')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailSearch'
	DROP Procedure ReleaseLogDetailSearch
END
GO

PRINT 'Creating Procedure ReleaseLogDetailSearch'
GO

/******************************************************************************
**		File: 
**		Description: ReleaseLogSearch
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
			EXEC ReleaseLogSearch NULL	, NULL	, NULL
			EXEC ReleaseLogSearch NULL	, 'K'	, NULL
			EXEC ReleaseLogSearch 1		, 'K'	, NULL
			EXEC ReleaseLogSearch 1		, NULL	, NULL
			EXEC ReleaseLogSearch NULL	, NULL	, 'W'

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
CREATE procedure dbo.ReleaseLogDetailSearch
(
   		@ApplicationId				INT 			= NULL
   	,	@ReleaseLogDetailId			INT				= NULL 	
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
	,	@SystemEntityType			VARCHAR(50)		= 'ReleaseLogDetail'
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				 = 1
	,	@AddTraceInfo				INT				 = 0
	,	@ReturnAuditInfo			INT				 = 0
	,	@StoredProcedureLogId		INT				= NULL	 
)

WITH RECOMPILE
AS
BEGIN

SET ANSI_NULLS ON; 

IF @AddTraceInfo = 1 
		BEGIN
	
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'ReleaseLogId' 
			SET @InputValuesLocal			= CAST(ISNULL(@ReleaseLogId, '%') AS VARCHAR(50))
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.ReleaseLogDetailSearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal	

		    DECLARE @StoredProcedureLogDetailId INT
			EXEC dbo.StoredProcedureLogDetailInsert
					@StoredProcedureLogDetailId		= @StoredProcedureLogDetailId OUTPUT
				,	@StoredProcedureLogId		= @StoredProcedureLogId
				,	@ParameterName  = 'ReleaseLogId'
				,   @ParameterValue = @ReleaseLogId
		
		END		

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
	
	
	
	IF LEN(LTRIM(RTRIM(@PrimaryEntity))) = 0 
	BEGIN
		SET	@PrimaryEntity = '%'
	END
	
	IF (@UpdatedDateRangeMin IS NULL OR @UpdatedDateRangeMin = '')
	BEGIN 
   
	
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
	AND		a.PrimaryDeveloper	LIKE @PrimaryDeveloper
	AND		a.JIRA				LIKE @JIRA 	 + '%'
	AND		a.Feature			LIKE @Feature 	 + '%'
	--AND		a.PrimaryEntity		LIKE @PrimaryEntity + '%'
	AND		a.ReleaseLogDetailId =
			CASE
				WHEN @ReleaseLogDetailId IS NULL THEN a.ReleaseLogDetailId
				ELSE @ReleaseLogDetailId
			END
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
  
 
  	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)	
  
	SELECT	Distinct	a.EntityKey   
	INTO	#TempDatelist
	FROM	CommonServices.dbo.AuditHistory a
	WHERE	(CONVERT(VARCHAR(10),CreatedDate ,101) >= CONVERT(VARCHAR(10),@UpdatedDateRangeMin ,101) 
	AND 	CONVERT(VARCHAR(10),CreatedDAte ,101) <= CONVERT(VARCHAR(10),@UpdatedDateRangeMax ,101))
	AND		a.SystemEntityId		 = 4100
	AND		a.AuditActionId IN (1,2)
	ORDER BY a.EntityKey  							ASC
  
  
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
	AND		a.PrimaryDeveloper	LIKE @PrimaryDeveloper
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
 	
   
 	
   	IF @AddAuditInfo = 1 
		BEGIN	
	
	-- Create Audit Record
	EXEC CommonServices.dbo.AuditHistoryInsert			
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= NULL
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
		END

END
GO


GO

