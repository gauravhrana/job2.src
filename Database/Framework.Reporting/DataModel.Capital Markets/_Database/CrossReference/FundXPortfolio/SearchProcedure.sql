IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FundXPortfolioSearch') 
BEGIN
	DROP Procedure FundXPortfolioSearch
END
GO

CREATE Procedure dbo.FundXPortfolioSearch
(
		@FundXPortfolioId				INT		= NULL
	,	@FundId				INT		= NULL
	,	@PortfolioId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'FundXPortfolio'
	,	@ApplicationMode				INT							= NULL
	,	@AddAuditInfo					INT							= 1
	,	@AddTraceInfo					INT							= 0
	,	@ReturnAuditInfo				INT							= 0
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
		SET @InputParametersLocal		=  'FundXPortfolioId' 
		SET @InputValuesLocal			=  CAST(@FundXPortfolioId AS VARCHAR(50))

		EXEC dbo.StoredProcedureLogInsert
				@Name					= 'dbo.FundXPortfolioSearch'
			,	@InputParameters		= @InputParametersLocal
			,	@InputValues			= @InputValuesLocal	
			-- TRACE --		

	END	

	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)


	SELECT 
			a. FundXPortfolioId
		,	a. FundId
		,	a. PortfolioId
		,	Fund.Name AS Fund
		,	Portfolio.Name AS Portfolio
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.FundXPortfolio a
	INNER JOIN Fund ON Fund.FundId = a.FundId
	INNER JOIN Portfolio ON Portfolio.PortfolioId = a.PortfolioId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	AND		a.FundXPortfolioId =
			CASE
				WHEN @FundXPortfolioId IS NULL THEN a.FundXPortfolioId
				ELSE @FundXPortfolioId
			END
	AND		a.FundId =
			CASE
				WHEN @FundId IS NULL THEN a.FundId
				ELSE @FundId
			END
	AND		a.PortfolioId =
			CASE
				WHEN @PortfolioId IS NULL THEN a.PortfolioId
				ELSE @PortfolioId
			END
	ORDER BY	a.FundXPortfolioId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE FundXPortfolioId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.FundXPortfolioId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.FundXPortfolioId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.FundXPortfolioId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.FundXPortfolioId=b.FundXPortfolioId
		ORDER BY	a.FundXPortfolioId
	END
	ELSE
	BEGIN
		DECLARE @StaticUpdatedDate AS DATETIME
		SET @StaticUpdatedDate = Convert(datetime, '1/1/1900', 103)

		SELECT	a.*
			,	UpdatedDate = @StaticUpdatedDate
			,	UpdatedBy	= 'Unknown'
			,	LastAction	= 'Unknown'
		FROM	#TempMain a	
		ORDER BY	a.FundXPortfolioId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @FundXPortfolioId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
