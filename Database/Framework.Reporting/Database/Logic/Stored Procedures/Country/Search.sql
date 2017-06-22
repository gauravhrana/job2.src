IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='CountrySearch')
BEGIN
	PRINT 'Dropping Procedure CountrySearch'
	DROP Procedure CountrySearch
END
GO

PRINT 'Creating Procedure CountrySearch'
GO

/******************************************************************************
**		File: 
**		Name: CountrySearch
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
			EXEC CountrySearch NULL	, NULL	, NULL
			EXEC CountrySearch NULL	, 'K'	, NULL
			EXEC CountrySearch 1	, 'K'	, NULL
			EXEC CountrySearch 1	, NULL	, NULL
			EXEC CountrySearch NULL	, NULL	, 'W'

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
Create procedure CountrySearch
(
		@CountryId					INT				= NULL 	
	,	@ApplicationId				INT				= NULL		
	,	@Name						VARCHAR(50)		= NULL
	,	@TimeZoneId					INT				= NULL 			
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'Country'
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
			@Name						= 'dbo.CountrySearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'	
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the Country did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.CountryId			
		,	a.ApplicationId
		,	a.Name						
		,	a.Description			
		,	a.SortOrder	
		,	a.TimeZoneId	
		,	b.Name					AS	'TimeZone'
	INTO	#TempMain
	FROM		dbo.Country		a
	INNER JOIN	Location.dbo.TimeZone	b	ON	a.TimeZoneId	=	b.TimeZoneId
	WHERE		a.Name				LIKE @Name	+ '%'
	AND			a.CountryId			= ISNULL(@CountryId,		a.CountryId)
	AND			a.TimeZoneId		= ISNULL(@TimeZoneId,		a.TimeZoneId)
	AND			a.ApplicationId		= ISNULL(@ApplicationId,	a.ApplicationId)
	ORDER BY a.SortOrder	ASC,
			 a.Name			ASC,
			 a.CountryId	ASC		
		IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE CountryId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
					 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.CountryId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.CountryId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.CountryId
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
				ON	a.CountryId	= b.CountryId
	ORDER BY	a.SortOrder				ASC
			,	a.CountryId
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
				,	a.CountryId
	END
	IF @AddAuditInfo = 1 
	BEGIN

	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'Country'
		,	@EntityKey				= @CountryId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END	

END
GO
	

