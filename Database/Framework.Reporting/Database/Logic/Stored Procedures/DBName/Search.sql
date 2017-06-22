IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='DBNameSearch')
BEGIN
	PRINT 'Dropping Procedure DBNameSearch'
	DROP Procedure DBNameSearch
END
GO

PRINT 'Creating Procedure DBNameSearch'
GO

/******************************************************************************
**		File: 
**		Name: DBNameSearch
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
			EXEC DBNameSearch NULL	, NULL	, NULL
			EXEC DBNameSearch NULL	, 'K'	, NULL
			EXEC DBNameSearch 1		, 'K'	, NULL
			EXEC DBNameSearch 1		, NULL	, NULL
			EXEC DBNameSearch NULL	, NULL	, 'W'

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
Create procedure DBNameSearch
(
		@DBNameId			INT				= NULL 	
	,	@ApplicationId	   	INT				= NULL		
	,	@Name			    VARCHAR(50)		= NULL 			
	,	@AuditId			INT								
	,	@AuditDate			DATETIME		= NULL			
	,	@SystemEntityType	VARCHAR(50)		= 'DBName'
	,	@ApplicationMode	INT				= NULL		
	,	@AddAuditInfo		INT				 = 1
	,	@AddTraceInfo		INT				 = 0
	,	@ReturnAuditInfo	INT				 = 0
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
				@Name						= 'dbo.DBNameSearch'
			,	@InputParameters			= @InputParametersLocal
			,	@InputValues				= @InputValuesLocal	
	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the DBName did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.DBNameId	
		,	a.ApplicationId	
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
	INTO	#TempMain
	FROM	dbo.DBName a	
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.DBNameId		= ISNULL(@DBNameId, a.DBNameId )
	AND a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SortOrder		ASC,
			 a.Name				ASC,
			 a.DBNameId	ASC			 
	  
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE DBNameId < 0
	END
		
	IF @ReturnAuditInfo = 1
	BEGIN
		-- get Audit latest record matching on key, systementitytype
		SELECT		c.EntityKey			
			,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
		INTO		#HistortyInfo
		FROM 		#TempMain a		
		INNER JOIN	CommonServices.dbo.AuditHistory c		
					ON	c.EntityKey			= a.DBNameId
					AND c.SystemEntityId	= @SystemEntityTypeId
					AND c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	
	
		-- Get Audit Date and CreatedByPersonId for given records
		SELECT		a.DBNameId	
				,	c.AuditActionId 
				,	c.CreatedDate
				,	c.CreatedByPersonId	
				, 	c.CreatedDate						AS	'UpdatedDate'
				,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
				,	d.Name								AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo										b
					ON	b.EntityKey			= a.DBNameId
		INNER JOIN	CommonServices.dbo.AuditHistory						c
					ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction						d	
					ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
					ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
		SELECT 	a.DBNameId
			,	a.ApplicationId			
			,	a.Name			
			,	a.[Description]		
			,	a.SortOrder			
			, 	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM #TempMain a
		LEFT JOIN #HistortyInfoDetails	b	
					ON	a.DBNameId	= b.DBNameId
		ORDER BY	a.SortOrder				ASC
				,	a.DBNameId
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
				,	a.DBNameId
	END
	
	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= 'DBName'
			,	@EntityKey				= @DBNameId
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId		
	END

END

GO

