IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='SuperKeySearch')
BEGIN
	PRINT 'Dropping Procedure SuperKeySearch'
	DROP Procedure SuperKeySearch
END
GO

PRINT 'Creating Procedure SuperKeySearch'
GO

/******************************************************************************
**		File: 
**		Name: SuperKeySearch
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
			EXEC SuperKeySearch NULL	, NULL	, NULL
			EXEC SuperKeySearch NULL	, 'K'	, NULL
			EXEC SuperKeySearch 1		, 'K'	, NULL
			EXEC SuperKeySearch 1		, NULL	, NULL
			EXEC SuperKeySearch NULL	, NULL	, 'W'

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
Create procedure SuperKeySearch
(
		@SuperKeyId					INT				= NULL 	
	,	@ApplicationId				INT				= NULL
	,	@SystemEntityTypeId			INT				= NULL		
	,	@Name						VARCHAR(50)		= NULL 		
	,	@ExpirationDate				DATETIME		= NULL
	,	@AuditId					INT								
	,	@AuditDate					DATETIME		= NULL			
	,	@SystemEntityType			VARCHAR(50)		= 'SuperKey'
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
			@Name						= 'dbo.SuperKeySearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		--,	@ExecutedBy					= 'System'
	END
	-- Get Main System Entity Type ID
	DECLARE @SystemEntityId AS INT
	Select @SystemEntityId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE

	-- if the SuperKey did not provide any values
	-- assume search on all possiblities ('%')
	SET @Name	= ISNULL(@Name, '%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(RTRIM(LTRIM(@Name))) = 0
		BEGIN
			SET	@NAME = '%'
		END
	
	SELECT	a.SuperKeyId
		,	a.ApplicationId					
		,	a.Name						
		,	a.Description					
		,	a.SortOrder
		,	a.SystemEntityTypeId
		,	a.ExpirationDate
		,	b.EntityName				AS	'SystemEntityType'
	INTO	#TempMain
	FROM		dbo.SuperKey a	
	INNER JOIN	Configuration.dbo.SystemEntityType b ON a.SystemEntityTypeId = b.SystemEntityTypeId
	WHERE	a.Name LIKE @Name	+ '%'
	AND		a.SuperKeyId		  = ISNULL(@SuperKeyId, a.SuperKeyId )
	AND		a.SystemEntityTypeId  = ISNULL(@SystemEntityTypeId, a.SystemEntityTypeId )
	AND		a.ApplicationId		  = ISNULL(@ApplicationId, a.ApplicationId )
	AND		a.ExpirationDate	  <= ISNULL(@ExpirationDate, a.ExpirationDate)
	ORDER BY a.SortOrder		ASC,
			 a.ExpirationDate	ASC,
			 a.SuperKeyId		ASC
	IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE SuperKeyId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN 
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.SuperKeyId
				AND c.SystemEntityId	= @SystemEntityId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.SuperKeyId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.SuperKeyId
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
				ON	a.SuperKeyId	= b.SuperKeyId
	ORDER BY	a.SortOrder				ASC
			,	a.SuperKeyId
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
				,	a.SuperKeyId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
	EXEC dbo.AuditHistoryInsert
			@SystemEntityType		= 'SuperKey'
		,	@EntityKey				= @SuperKeyId
		,	@AuditAction			= 'Search'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId	
	END
END
GO
	

