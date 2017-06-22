IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name ='ReleaseLogDetailSearch2')
BEGIN
	PRINT 'Dropping Procedure ReleaseLogDetailSearch2'
	DROP Procedure ReleaseLogDetailSearch2
END
GO

PRINT 'Creating Procedure ReleaseLogDetailSearch2'
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
CREATE procedure dbo.ReleaseLogDetailSearch2
(
		@ReleaseLogDetailId			INT				= NULL 	
	,	@ApplicationId				INT				= NULL
	,	@ReleaseLogId				INT				= NULL  
	,	@ReleaseLogStatusId			INT				= NULL  
	,	@Description				VARCHAR(50)		= NULL 	
	,	@ModuleId					INT				= NULL
	,	@PrimaryDeveloper			VARCHAR(MAX)	= NULL
	,	@JIRA						VARCHAR(MAX)	= NULL
	,	@Feature					VARCHAR(MAX)	= NULL
	,	@PrimaryEntity				VARCHAR(MAX)	= NULL
	,	@ReleaseIssueTypeId			INT				= NULL
	,	@ReleasePublishCategoryId	INT				= NULL
	,	@UpdatedDateRangeMin		DATETIME		= NULL 
	,	@UpdatedDateRangeMax		DATETIME		= NULL 
	,	@AuditId					INT					
	,	@ShowResults				INT				= NULL	
	,	@AuditDate					DATETIME		= NULL
	,	@StoredProcedureLogId		INT				= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'ReleaseLogDetail'	
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				 = 1
	,	@AddTraceInfo				INT				 = 0
	,	@ReturnAuditInfo			INT				 = 0
)
WITH RECOMPILE
AS
BEGIN

	SET	NOCOUNT ON

	IF @AddTraceInfo = 1 
		BEGIN
		
			DECLARE @InputParametersLocal	VARCHAR(500)  
			DECLARE @InputValuesLocal		VARCHAR(5000)  
			SET @InputParametersLocal		= 'ReleaseLogId' 
			SET @InputValuesLocal			= CAST(ISNULL(@ReleaseLogId, '%') AS VARCHAR(50))
			EXEC dbo.StoredProcedureLogInsert
					@Name						= 'dbo.ReleaseLogDetailSearch2'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal	

		    DECLARE @StoredProcedureLogDetailId INT
			EXEC dbo.StoredProcedureLogDetailInsert
					@StoredProcedureLogDetailId		= @StoredProcedureLogDetailId OUTPUT
				,	@StoredProcedureLogId		= @StoredProcedureLogId
				,	@ParameterName  = 'ReleaseLogId'
				,   @ParameterValue = @ReleaseLogId
		
		END	
	
	-- if the ReleaseLog did not provide any values
	-- assume search on all possiblities ('%')
	SET @Description	= ISNULL(@Description, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Description))) = 0
		BEGIN
			SET	@Description = '%'
		END

	-- assume search on all possiblities ('%')
	SET	@PrimaryDeveloper = ISNULL(@PrimaryDeveloper,'%')
	SET	@JIRA = ISNULL(@JIRA,'%')
	SET	@Feature = ISNULL(@Feature,'%')
	SET	@PrimaryEntity = ISNULL(@PrimaryEntity,'%')
	
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

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)	
	
	 SELECT Distinct	a.EntityKey   
	INTO #TempDatelist
	FROM	dbo.AuditHistory a
	WHERE		 
	(CONVERT(VARCHAR(10),CreatedDate ,101) >= CONVERT(VARCHAR(10),@UpdatedDateRangeMin ,101) AND 
	 CONVERT(VARCHAR(10),CreatedDAte ,101) <= CONVERT(VARCHAR(10),@UpdatedDateRangeMax ,101))
	AND		a.SystemEntityId		 = 4100
	AND a.AuditActionId IN (1,2)
	ORDER BY a.EntityKey  							ASC
  
  IF object_id('tempdb..#ReleaseLogDetailSearch_Result') IS NULL

	BEGIN

	CREATE TABLE #ReleaseLogDetailSearch_Result
	(
	ReleaseLogDetailId       INT NOT NULL,
	ApplicationId            INT NOT NULL,
	ReleaseLogId             INT NOT NULL,
	ItemNo                   INT NOT NULL,
	Description              VARCHAR (50) NOT NULL,
	SortOrder                INT NOT NULL,
	RequestedBy              VARCHAR (50) NOT NULL,
	PrimaryDeveloper         VARCHAR (50) NOT NULL,
	RequestedDate            DATETIME NOT NULL,
	ReleaseIssueTypeId       INT NOT NULL,
	ReleasePublishCategoryId INT NOT NULL,	
	JIRA                     VARCHAR (50) NOT NULL,
	Feature                  VARCHAR (255) NOT NULL,
	PrimaryEntity            VARCHAR (225) NOT NULL,
	ReleaseLog       		 VARCHAR (150) NOT NULL,
	ReleaseIssueType       	 VARCHAR (150) NOT NULL,
	ReleasePublishCategory 	 VARCHAR (150) NOT NULL,
	Name 					 VARCHAR (150) NOT NULL,
	ReleaseLogStatus       	 VARCHAR (150) NOT NULL
	
	)

	END
	
	INSERT INTO	#ReleaseLogDetailSearch_Result 
	SELECT	a.ReleaseLogDetailId
		,	a.ApplicationId	
	    ,   a.ReleaseLogId              
		,	a.ItemNo                    
		,	a.Description				
		,	a.SortOrder					
		,	a.RequestedBy               
		,	a.PrimaryDeveloper          
		,	a.RequestedDate
		,	a.ReleaseIssueTypeId			
		,	a.ReleasePublishCategoryId	
		,	a.JIRA						
		,	a.Feature					
		,	a.PrimaryEntity						
		,	b.Name							AS	'ReleaseLog'
		,	c.Name							AS	'ReleaseIssueType'		
		,	d.Name							AS	'ReleasePublishCategory'
		,	a.Description					AS	'Name'
		,	e.Name							AS 	'ReleaseLogStatus'
	
	FROM		dbo.ReleaseLogDetail		a
	INNER JOIN	dbo.ReleaseLog				b 
		ON	a.ReleaseLogId = b.ReleaseLogId
	  INNER JOIN	dbo.ReleaseLogStatus				e 
		ON	b.ReleaseLogStatusId = e.ReleaseLogStatusId	
	INNER JOIN	dbo.ReleaseIssueType		c
		ON	a.ReleaseIssueTypeId = c.ReleaseIssueTypeId
	INNER JOIN	dbo.ReleasePublishCategory	d
		ON	a.ReleasePublishCategoryId = d.ReleasePublishCategoryId
		
	WHERE	a.PrimaryDeveloper	LIKE @PrimaryDeveloper + '%'
	AND		a.JIRA				LIKE @JIRA 	 + '%'
	AND		a.Feature			LIKE @Feature 	 + '%'
	AND		a.PrimaryEntity		LIKE @PrimaryEntity + '%'
	AND		a.Description		LIKE @Description	+ '%'
	AND		a.ApplicationId =
			CASE
				WHEN @ApplicationId IS NULL THEN a.ApplicationId
				ELSE @ApplicationId
			END
	AND		a.ReleaseLogId =
			CASE
				WHEN @ReleaseLogId IS NULL THEN a.ReleaseLogId 
				ELSE @ReleaseLogId
			END
				AND		b.ReleaseLogStatusId =
			CASE
				WHEN @ReleaseLogStatusId IS NULL THEN b.ReleaseLogStatusId 
				ELSE @ReleaseLogStatusId
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
	AND		a.ReleaseLogDetailId =
			CASE
				WHEN @ReleaseLogDetailId IS NULL THEN a.ReleaseLogDetailId
				ELSE @ReleaseLogDetailId
			END
	ORDER BY a.SortOrder				ASC
		,	 a.ReleaseLogDetailId		ASC
		,	 a.ReleaseLogId				ASC

	IF @ShowResults = 1
	BEGIN
		SELECT	ReleaseLogDetailId
		,	ApplicationId	
	    ,   ReleaseLogId              
		,	ItemNo                    
		,	Description				
		,	SortOrder					
		,	RequestedBy               
		,	PrimaryDeveloper          
		,	RequestedDate
		,	ReleaseIssueTypeId			
		,	ReleasePublishCategoryId	
		,	JIRA						
		,	Feature					
		,	PrimaryEntity						
		,	ReleaseLog
		,	ReleaseIssueType		
		,	ReleasePublishCategory
		,	Name
		,	ReleaseLogStatus
	FROM	#ReleaseLogDetailSearch_Result 
	END
	
END
GO

