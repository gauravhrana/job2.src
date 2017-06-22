IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TaskScheduleSearch')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleSearch'
	DROP Procedure TaskScheduleSearch
END
GO

PRINT 'Creating Procedure TaskScheduleSearch'
GO

/******************************************************************************
**		File: 
**		Name: TaskScheduleSearch
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
			EXEC TaskScheduleSearch NULL	, NULL	, NULL
			EXEC TaskScheduleSearch NULL	, 'K'	, NULL
			EXEC TaskScheduleSearch 1		, 'K'	, NULL
			EXEC TaskScheduleSearch 1		, NULL	, NULL
			EXEC TaskScheduleSearch NULL	, NULL	, 'W'

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
Create procedure dbo.TaskScheduleSearch
(
		@TaskScheduleId			INT				= NULL 
	,	@ApplicationId			INT				= NULL			
	,	@TaskScheduleTypeId		INT				= NULL 	
	,   @TaskEntityId			INT				= NULL 		
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	 
	,	@SystemEntityType		VARCHAR(50)		= 'TaskSchedule'
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
	SET @InputParametersLocal		= 'TaskScheduleTypeId' + ', '+ 'TaskEntityId'
	SET @InputValuesLocal			= CAST(@TaskScheduleTypeId AS VARCHAR(50)) + ', ' + CAST(@TaskEntityId AS VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert
			@Name				= 'dbo.TaskScheduleSearch'	
		,	@InputParameters	= @InputParametersLocal
		,	@InputValues		= @InputValuesLocal		
	END
   
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	--SET @TaskEntity	= ISNULL(@TaskEntity, '%')

	----if blank, then assume search on all possiblities ('%')
	--IF LEN(RTRIM(LTRIM(@TaskEntity))) = 0
	--	BEGIN
	--		SET	@TaskEntity = '%'
	--	END

	--SET @TaskScheduleType	= ISNULL(@TaskScheduleType, '%')

	----if blank, then assume search on all possiblities ('%')
	--IF LEN(RTRIM(LTRIM(@TaskScheduleType))) = 0
	--	BEGIN
	--		SET	@TaskScheduleType = '%'
	--	END

	SELECT	a.TaskScheduleId	
		,	a.ApplicationId	
		,	a.TaskScheduleTypeId		
		,	a.TaskEntityId		
		,	b.Name		AS	'TaskScheduleType'				
		,	c.Name		AS	'TaskEntity'
	INTO		#TempMain	
	FROM	TaskSchedule a
	INNER JOIN	dbo.TaskScheduleType b ON a.TaskScheduleTypeId	= b.TaskScheduleTypeId
	INNER JOIN	dbo.TaskEntity		 c ON a.TaskEntityId		= c.TaskEntityId
	WHERE a.TaskScheduleTypeId	  = ISNULL(@TaskScheduleTypeId, a.TaskScheduleTypeId)
	AND a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	AND a.TaskEntityId	  = ISNULL(@TaskEntityId, a.TaskEntityId)
	AND a.TaskScheduleId	  = ISNULL(@TaskScheduleId, a.TaskScheduleId)
	ORDER BY a.TaskScheduleId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE TaskScheduleId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.TaskScheduleId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.TaskScheduleId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.TaskScheduleId
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
				ON	a.TaskScheduleId	= b.TaskScheduleId
	ORDER BY	a.TaskScheduleId
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
				,	a.TaskScheduleId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
		END

END
GO
	

