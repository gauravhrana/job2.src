IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TaskEntitySearch')
BEGIN
	PRINT 'Dropping Procedure TaskEntitySearch'
	DROP Procedure TaskEntitySearch
END
GO

PRINT 'Creating Procedure TaskEntitySearch'
GO

/******************************************************************************
**		File: 
**		Name: TaskEntitySearch
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
			EXEC TaskEntitySearch NULL	, NULL	, NULL
			EXEC TaskEntitySearch NULL	, 'K'	, NULL
			EXEC TaskEntitySearch 1		, 'K'	, NULL
			EXEC TaskEntitySearch 1		, NULL	, NULL
			EXEC TaskEntitySearch NULL	, NULL	, 'W'

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
Create procedure dbo.TaskEntitySearch
(
		@TaskEntityId			INT				= NULL 
	,	@Name					VARCHAR(50)		= NULL 	
	,	@ApplicationId			INT				= NULL
	,	@TaskEntityTypeId		INT				= NULL 	
	,	@Active					INT				= NULL 	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'TaskEntity'
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
	SET @InputParametersLocal		= 'Name' + ', '+ 'TaskEntityTypeId'
	SET @InputValuesLocal			= @Name + ', ' + CAST(@TaskEntityTypeId AS VARCHAR(50))
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.TaskEntitySearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	
	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	--SET @TaskEntityType	= ISNULL(@TaskEntityType, '%')

	--if blank, then assume search on all possiblities ('%')
	--IF LEN(RTRIM(LTRIM(@TaskEntityType))) = 0
	--	BEGIN
	--		SET	@TaskEntityType = '%'
	--	END

	SELECT	a.TaskEntityId		
		,	a.ApplicationId 
		,	a.Name				 
		,	a.TaskEntityTypeId	 
		,	a.Description		 
		,	a.Active			 
		,	a.SortOrder
		,	b.Name		AS	'TaskEntityType'
	INTO		#TempMain
	FROM		dbo.TaskEntity a
	INNER JOIN	dbo.TaskEntityType b ON a.TaskEntityTypeId = b.TaskEntityTypeId
	WHERE	a.Name LIKE @Name			+ '%'
	--AND		b.Name LIKE @TaskEntityType	+ '%'
	AND a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	AND a.TaskEntityTypeId	  = ISNULL(@TaskEntityTypeId, a.TaskEntityTypeId)
	AND a.TaskEntityId	  = ISNULL(@TaskEntityId, a.TaskEntityId)
	AND a.Active	  = ISNULL(@Active, a.Active)
	ORDER BY a.SortOrder	ASC
		,	 a.TaskEntityId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE TaskEntityId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN

	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.TaskEntityId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.TaskEntityId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.TaskEntityId
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
				ON	a.TaskEntityId	= b.TaskEntityId
	ORDER BY	a.SortOrder				ASC
			,	a.TaskEntityId
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
					,	a.TaskEntityId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskEntityId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END
END
GO
	

