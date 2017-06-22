﻿IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TaskScheduleTypeSearch')
BEGIN
	PRINT 'Dropping Procedure TaskScheduleTypeSearch'
	DROP Procedure TaskScheduleTypeSearch
END
GO

PRINT 'Creating Procedure TaskScheduleTypeSearch'
GO

/******************************************************************************
**		File: 
**		Name: TaskScheduleTypeSearch
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
			EXEC TaskScheduleTypeSearch NULL	, NULL	, NULL
			EXEC TaskScheduleTypeSearch NULL	, 'K'	, NULL
			EXEC TaskScheduleTypeSearch 1		, 'K'	, NULL
			EXEC TaskScheduleTypeSearch 1		, NULL	, NULL
			EXEC TaskScheduleTypeSearch NULL	, NULL	, 'W'

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
Create procedure dbo.TaskScheduleTypeSearch
(
		@TaskScheduleTypeId		INT				= NULL 
	,	@ApplicationId			INT				= NULL 
	,	@Name					VARCHAR(50)		= NULL 	
	,	@Active					INT				= NULL 	
	,	@AuditId				INT							
	,	@AuditDate				DATETIME		= NULL
	,	@SystemEntityType		VARCHAR(50)		= 'TaskScheduleType'
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
	SET @InputParametersLocal		= 'Name' 
	SET @InputValuesLocal			= @Name
	EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.TaskScheduleTypeSearch'	
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal
	END
    
	-- TRACE AND LOGGING ---
	
	-- -- -- -- -- -- 
	-- CORE CODE ---
	-- -- -- -- -- -- 
	
	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	

	SELECT	a.*
	FROM	dbo.TaskScheduleType a
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.TaskScheduleTypeId	  = ISNULL(@TaskScheduleTypeId, a.TaskScheduleTypeId)
	AND a.ApplicationId	  = ISNULL(@ApplicationId, a.ApplicationId)
	AND a.Active	  = ISNULL(@Active, a.Active)
	ORDER BY a.SortOrder			ASC
		,	 a.TaskScheduleTypeId	ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE TaskScheduleTypeId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.TaskScheduleTypeId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.TaskScheduleTypeId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.TaskScheduleTypeId
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
				ON	a.TaskScheduleTypeId	= b.TaskScheduleTypeId
	ORDER BY	a.SortOrder				ASC
			,	a.TaskScheduleTypeId
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
				,	a.TaskScheduleTypeId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TaskScheduleTypeId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId
	END

END
GO 
	

