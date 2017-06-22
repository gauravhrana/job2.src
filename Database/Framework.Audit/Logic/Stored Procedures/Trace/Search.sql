IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='TraceSearch')
BEGIN
	PRINT 'Dropping Procedure TraceSearch'
	DROP Procedure TraceSearch
END
GO

PRINT 'Creating Procedure TraceSearch'
GO

/******************************************************************************
**		File: 
**		Name: TraceSearch
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
			EXEC TraceSearch NULL	, NULL	, NULL
			EXEC TraceSearch NULL	, 'K'	, NULL
			EXEC TraceSearch 1		, 'K'	, NULL
			EXEC TraceSearch 1		, NULL	, NULL
			EXEC TraceSearch NULL	, NULL	, 'W'

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
Create procedure TraceSearch
(
		@TraceId				INT				= NULL 	
	,	@ApplicationId			INT				= NULL		
	,	@Name					VARCHAR(50)		= NULL 			
	,	@AuditId				INT								
	,	@AuditDate				DATETIME		= NULL			
	,	@SystemEntityType		VARCHAR(50)		= 'Trace'	
	,	@ApplicationMode		INT				= NULL		
	,	@AddAuditInfo			INT				 = 1
	,	@AddTraceInfo			INT				 = 0
	,	@ReturnAuditInfo		INT				 = 0
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
					@Name						= 'dbo.TraceSearch'
				,	@InputParameters			= @InputParametersLocal
				,	@InputValues				= @InputValuesLocal	
				--,	@ExecutedBy					= 'System'				
		
		END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the Trace did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.TraceId	
		,	a.ApplicationId	
		,	a.Name				
		,	a.Description			
		,	a.SortOrder	
	INTO	#TempMain
	FROM	dbo.Trace a	
	WHERE	a.Name LIKE @Name	+ '%'
	AND a.TraceId		= ISNULL(@TraceId, a.TraceId )
	AND a.ApplicationId			= ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY a.SortOrder	ASC,
			 a.Name			ASC,
			 a.TraceId		ASC	
			 
	
	IF @ReturnAuditInfo = 1
		BEGIN	 
		
			-- get Audit latest record matching on key, systementitytype
			SELECT		c.EntityKey			
				,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
			INTO		#HistortyInfo
			FROM 		#TempMain a		
			INNER JOIN	CommonServices.dbo.AuditHistory c		
						ON	c.EntityKey			= a.TraceId
						AND c.SystemEntityId	= @SystemEntityTypeId
						AND c.AuditActionId		IN (1,2)
			GROUP BY	c.EntityKey	
	
			-- Get Audit Date and CreatedByPersonId for given records
			SELECT		a.TraceId	
					,	c.AuditActionId 
					,	c.CreatedDate
					,	c.CreatedByPersonId	
					, 	c.CreatedDate						AS	'UpdatedDate'
					,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
					,	d.Name								AS	'LastAction'
			INTO		#HistortyInfoDetails
			FROM		#TempMain a
			INNER JOIN	#HistortyInfo										b
						ON	b.EntityKey			= a.TraceId
			INNER JOIN	CommonServices.dbo.AuditHistory						c
						ON	c.AuditHistoryId	= b.MaxAuditHistoryId
			INNER JOIN	CommonServices.dbo.AuditAction						d	
						ON	c.AuditActionId 	= d.AuditActionId
			INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e	
						ON	c.CreatedByPersonId	= e.ApplicationUserId		
	
			SELECT 	a.TraceId
				,	a.ApplicationId			
				,	a.Name			
				,	a.Description		
				,	a.SortOrder			
				, 	b.UpdatedDate
				,	b.UpdatedBy
				,	b.LastAction
			FROM #TempMain a
			LEFT JOIN #HistortyInfoDetails	b	
						ON	a.TraceId	= b.TraceId
			ORDER BY	a.SortOrder				ASC
					,	a.TraceId
		
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
				,	a.TraceId

		END	

	IF @AddAuditInfo = 1 
		BEGIN

			-- Create Audit Record
			EXEC dbo.AuditHistoryInsert
					@SystemEntityType		= 'Trace'
				,	@EntityKey				= @TraceId
				,	@AuditAction			= 'Search'
				,	@CreatedDate			= @AuditDate
				,	@CreatedByPersonId		= @AuditId	
		
		END

END
GO
	

