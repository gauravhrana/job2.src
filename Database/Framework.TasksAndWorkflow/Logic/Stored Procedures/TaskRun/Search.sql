IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TaskRunSearch')
BEGIN
	PRINT 'Dropping Procedure TaskRunSearch'
	DROP Procedure TaskRunSearch
END
GO

PRINT 'Creating Procedure TaskRunSearch'
GO

/******************************************************************************
**		File: 
**		Name: TaskRunSearch
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
			EXEC TaskRunSearch NULL	, NULL	, NULL
			EXEC TaskRunSearch NULL	, 'K'	, NULL
			EXEC TaskRunSearch 1	, 'K'	, NULL
			EXEC TaskRunSearch 1	, NULL	, NULL
			EXEC TaskRunSearch NULL	, NULL	, 'W'

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
Create procedure dbo.TaskRunSearch
(
		@TaskRunId				INT				= NULL 		
	,	@TaskEntityId			INT				= NULL	
	,	@TaskScheduleId			INT				= NULL	
	,	@ApplicationId			INT				= NULL	
	,	@RunBy					VARCHAR(50)		= NULL		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'TaskRun'
	,	@ApplicationMode					INT				= NULL		
	,	@AddAuditInfo						INT				 = 1
	,	@AddTraceInfo						INT				 = 0
	,	@ReturnAuditInfo					INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON
	IF @AddTraceInfo = 1 
	BEGIN
	DECLARE @InputParametersLocal	VARCHAR(500)  
	DECLARE @InputValuesLocal		VARCHAR(5000)  
	SET @InputParametersLocal		= 'TaskScheduleId' + ', '+ 'TaskEntityId'
	SET @InputValuesLocal			= CAST(@TaskScheduleId AS VARCHAR(50)) + ', ' + CAST(@TaskEntityId AS VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.TaskEntityTypeSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
	END

	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	--SET @TaskEntity	= ISNULL(@TaskEntity, '%')

	----if blank, then assume search on all possiblities ('%')
	--IF LEN(RTRIM(LTRIM(@TaskEntity))) = 0
	--	BEGIN
	--		SET	@TaskEntity = '%'
	--	END

	SET @RunBy	= ISNULL(@RunBy, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@RunBy))) = 0
		BEGIN
			SET	@RunBy = '%'
		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)	

	SELECT	a.TaskRunId	
		,	a.ApplicationId	
		,	a.TaskScheduleId	
		,	a.TaskEntityId	
		,	CONVERT (datetime,convert(char(8),a.BusinessDate )) AS BusinessDate	
		,	a.StartTime		
		,	a.EndTime		
		,	a.RunBy				
		,	b.Name		AS	'TaskEntity'
	INTO		#TempMain
	FROM	TaskRun a
	INNER JOIN	dbo.TaskEntity b ON a.TaskEntityId		= b.TaskEntityId
	WHERE	a.TaskScheduleId	=	ISNULL(@TaskScheduleId, a.TaskScheduleId)
	AND		a.RunBy LIKE	@RunBy				+ '%'
	AND a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	AND a.TaskEntityId	  = ISNULL(@TaskEntityId, a.TaskEntityId)
	AND a.TaskRunId		  = ISNULL(@TaskRunId, a.TaskRunId)
	ORDER BY a.TaskRunId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE TaskRunId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.TaskRunId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.TaskRunId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.TaskRunId
	INNER JOIN	CommonServices.dbo.AuditHistory						c
				ON	c.AuditHistoryId	= b.MaxAuditHistoryId
	INNER JOIN	CommonServices.dbo.AuditAction						d	
				ON	c.AuditActionId 	= d.AuditActionId
	INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
				ON	c.CreatedByPersonId	= e.ApplicationUserId	
	
	SELECT 	a.*		
		, 	b.UpdatedDate
		,	b.UpdatedBy
		,	b.LastAction
	FROM #TempMain a
	LEFT JOIN #HistortyInfoDetails	b	
				ON	a.TaskRunId	= b.TaskRunId
	ORDER BY	a.TaskRunId
	END
	ELSE
	BEGIN
		DECLARE @StaticUpdatedDate AS DATETIME
		SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)
	
		SELECT 	a.*
		   	,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a		
		ORDER BY	a.SortOrder				ASC
				,	a.TaskRunId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskRunId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO
	

