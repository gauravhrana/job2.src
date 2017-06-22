IF EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name = 'AuditActionSearch')
BEGIN
	PRINT 'Dropping Procedure AuditActionSearch'
	DROP  Procedure  AuditActionSearch
END
GO

PRINT 'Creating Procedure AuditActionSearch'
GO

/******************************************************************************
**		File: 
**		Name: AuditActionSearch
**		Desc: 
**
**		This template can be customized:
**              
**		Return values:
** 
**		Called by:   
**              
**		Parameters:
**		Input							Output
**     ----------					   ---------
**
**		Auth: 
**		Date: 
*******************************************************************************
**		Change History
*******************************************************************************
**		Date:		Author:				Description:
**		--------		--------				-------------------------------------------
**    
*******************************************************************************/
CREATE Procedure dbo.AuditActionSearch
(
		@AuditActionId			INT				= NULL	
	,	@ApplicationId			INT				= NULL
	,	@Name					VARCHAR(50)		= NULL	
	,	@AuditId				INT						
	,	@AuditDate				DATETIME		= NULL	
	,	@SystemEntityType		VARCHAR(50)		= 'AuditAction'		
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

		-- TRACE AND LOGGING ---	
		DECLARE @InputParametersLocal	VARCHAR(500)  
		DECLARE @InputValuesLocal		VARCHAR(5000)  
		SET @InputParametersLocal		= 'Name' 
		SET @InputValuesLocal			= @Name  
		EXEC dbo.StoredProcedureLogInsert
				@Name						= 'dbo.AuditActionSearch'
			,	@InputParameters			= @InputParametersLocal
			,	@InputValues				= @InputValuesLocal	
			--,	@ExecutedBy					= 'System'	

	END

	-- Get Main System Entity Type ID
	DECLARE @SystemEntityTypeId AS INT
	Select @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)
	-- TRACE


	-- if the client did not provide any values
	-- assume search on all possiblities ('%')
	SET	@Name = ISNULL(@Name,'%')

	--if blank, then assume search on all possiblities ('%')
	IF LEN(LTRIM(RTRIM(@Name))) = 0 
	BEGIN
		SET	@Name = '%'
	END

	SELECT		a.*	
	INTO		#TempMain
	FROM		dbo.AuditAction a
	WHERE		a.Name LIKE @Name + '%'
	AND a.AuditActionId	 = ISNULL(@AuditActionId, a.AuditActionId )
	AND a.ApplicationId	 = ISNULL(@ApplicationId, a.ApplicationId )
	ORDER BY	a.SortOrder		ASC
		,		a.AuditActionId	ASC

	IF @ReturnAuditInfo = 1
		BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT		c.EntityKey			
			,		MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'				 
		INTO		#HistortyInfo
		FROM 		#TempMain a		
		INNER JOIN	CommonServices.dbo.AuditHistory c		
					ON	c.EntityKey			= a.AuditActionId
					AND c.SystemEntityId	= @SystemEntityTypeId
					AND c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	
	
		-- Get Audit Date and CreatedByPersonId for given records
		SELECT		a.AuditActionId	
				,	c.CreatedDate
				,	c.CreatedByPersonId	
				, 	c.CreatedDate						AS	'UpdatedDate'
				,	e.FirstName + ' ' + e.LastName		AS	'UpdatedBy'
				,	d.Name								AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo										b
					ON	b.EntityKey			= a.AuditActionId
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
					ON	a.AuditActionId	= b.AuditActionId
		ORDER BY	a.SortOrder				ASC
				,	a.AuditActionId

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
					,	a.AuditActionId

		END

	IF @AddAuditInfo = 1 
		BEGIN

		--Create Audit Record
		EXEC dbo.AuditHistoryInsert
				@SystemEntityType		= @SystemEntityType
			,	@EntityKey				= @AuditActionId
			,	@AuditAction			= 'Search'
			,	@CreatedDate			= @AuditDate
			,	@CreatedByPersonId		= @AuditId

		END

END		
GO