IF EXISTS (SELECT * FROM sysobjects WHERE type='P' AND name='ApplicationOperationSearch')
BEGIN
	PRINT 'Dropping Procedure ApplicationOperationSearch'
	DROP Procedure ApplicationOperationSearch
END
GO

PRINT 'Creating Procedure ApplicationOperationSearch'
GO

/******************************************************************************
**		File: 
**		Name: ApplicationOperationSearch
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
			EXEC ApplicationOperationSearch NULL	, NULL	, NULL
			EXEC ApplicationOperationSearch NULL	, 'K'	, NULL
			EXEC ApplicationOperationSearch 1		, 'K'	, NULL
			EXEC ApplicationOperationSearch 1		, NULL	, NULL
			EXEC ApplicationOperationSearch NULL	, NULL	, 'W'

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
Create procedure ApplicationOperationSearch
(
		@ApplicationOperationId		INT				= NULL 	
	,	@Name						VARCHAR(50)		= NULL 
	,	@Description				VARCHAR(100)	= NULL	
	,	@ApplicationId				INT				= NULL	
	,	@SystemEntityId				INT				= NULL
	,	@AuditId					INT						
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'ApplicationOperation' 
	,	@ApplicationMode			INT				= NULL		
	,	@AddAuditInfo				INT				 = 1
	,	@AddTraceInfo				INT				 = 0
	,	@ReturnAuditInfo			INT				 = 0	
)
WITH RECOMPILE
AS
BEGIN

	SET  NOCOUNT ON

	IF @AddTraceInfo = 1 
	BEGIN
		-- TRACE --
		DECLARE @InputParametersLocal	VARCHAR(500)  
		DECLARE @InputValuesLocal		VARCHAR(5000)  

		SET @InputParametersLocal		= 'ApplicationOperationId' + ', ' + 'Name' + ', ' + 'Description' 
		SET @InputValuesLocal			= CAST(@ApplicationOperationId AS VARCHAR(50)) + ', '+ ISNULL(@Name, 'NULL') + ', '+ ISNULL(@Description, 'NULL') 

		EXEC dbo.StoredProcedureLogInsert
			@Name						= 'dbo.ApplicationOperationSearch'
		,	@InputParameters			= @InputParametersLocal
		,	@InputValues				= @InputValuesLocal	
		-- TRACE --		
	END 

	
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)

	--if blank, then assume search on all possiblities ('%')
	IF  @Name IS NULL OR LEN(RTRIM(LTRIM(@Name))) = 0
	BEGIN
		SET	@NAME = '%'
	END

	--if blank, then assume search on all possiblities ('%')
	IF  @Description IS NULL OR LEN(RTRIM(LTRIM(@Description))) = 0
	BEGIN
		SET	@Description = '%'
	END
		
	SELECT	a.ApplicationOperationId			
		,	a.Name									
		,	a.Description						
		,	a.SortOrder						
		,	a.ApplicationId	
		,	b.Name					AS 'Application'			
		,	a.SystemEntityTypeId				
		,	c.EntityName			AS 'SystemEntityType'
		,	a.OperationValue
	INTO	#TempMain			
	FROM	dbo.ApplicationOperation a	
	INNER JOIN dbo.Application						b ON a.ApplicationId		= b.ApplicationId
	INNER JOIN Configuration.dbo.SystemEntityType   c ON a.SystemEntityTypeId	= c.SystemEntityTypeId
	WHERE	a.Name LIKE @Name	+ '%'
	AND		a.ApplicationId			 = ISNULL(@ApplicationId, a.ApplicationId )
	AND		a.SystemEntityTypeId	 = ISNULL(@SystemEntityId, a.SystemEntityTypeId )
	AND		a.ApplicationOperationId = ISNULL(@ApplicationOperationId, a.ApplicationOperationId )
	ORDER BY a.SortOrder	ASC
		,	 a.Name			ASC
		,	 a.ApplicationOperationId	ASC

IF	@ApplicationMode = 1 
	BEGIN		
		DELETE FROM #TempMain
		WHERE ApplicationOperationId < 0
	END
			
	IF @ReturnAuditInfo = 1
	BEGIN
		
	-- get Audit latest record matching on key, systementitytype
	SELECT		c.EntityKey			
		,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
	INTO		#HistortyInfo
	FROM 		#TempMain a		
	INNER JOIN	CommonServices.dbo.AuditHistory c		
				ON	c.EntityKey			= a.ApplicationOperationId
				AND c.SystemEntityId	= @SystemEntityTypeId
				AND c.AuditActionId		IN (1,2)
	GROUP BY	c.EntityKey	
	
	-- Get Audit Date and CreatedByPersonId for given records
	SELECT		a.ApplicationOperationId	
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId	
			, 	c.CreatedDate						AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
			,	d.Name								AS	'LastAction'
	INTO		#HistortyInfoDetails
	FROM		#TempMain a
	INNER JOIN	#HistortyInfo										b
				ON	b.EntityKey			= a.ApplicationOperationId
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
				ON	a.ApplicationOperationId	= b.ApplicationOperationId
	ORDER BY	a.SortOrder				ASC
			,	a.ApplicationOperationId
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
				,	a.ApplicationOperationId
	END
	IF @AddAuditInfo = 1 
	BEGIN
	-- Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= 'ApplicationOperation'
			,	@EntityKey				= @ApplicationOperationId
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId	
	END
END
GO
	
--EXEC ApplicationOperationSearch @AuditId = 5, @Name = 'ApplicationOperation9'
--SELECT * FROM ApplicationOperation
