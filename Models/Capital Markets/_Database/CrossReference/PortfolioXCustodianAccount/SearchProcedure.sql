IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PortfolioXCustodianAccountSearch') 
BEGIN
	DROP Procedure PortfolioXCustodianAccountSearch
END
GO

CREATE Procedure dbo.PortfolioXCustodianAccountSearch
(
		@PortfolioXCustodianAccountId				INT		= NULL
	,	@CustodianAccountId				INT		= NULL
	,	@PortfolioId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'PortfolioXCustodianAccount'
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
		SET @InputParametersLocal		=  'PortfolioXCustodianAccountId' 
		SET @InputValuesLocal			=  CAST(@PortfolioXCustodianAccountId AS VARCHAR(50))

		EXEC dbo.StoredProcedureLogInsert
				@Name					= 'dbo.PortfolioXCustodianAccountSearch'
			,	@InputParameters		= @InputParametersLocal
			,	@InputValues			= @InputValuesLocal	
			-- TRACE --		

	END	

	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)


	SELECT 
			a. PortfolioXCustodianAccountId
		,	a. CustodianAccountId
		,	a. PortfolioId
		,	CustodianAccount.Name AS CustodianAccount
		,	Portfolio.Name AS Portfolio
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.PortfolioXCustodianAccount a
	INNER JOIN CustodianAccount ON CustodianAccount.CustodianAccountId = a.CustodianAccountId
	INNER JOIN Portfolio ON Portfolio.PortfolioId = a.PortfolioId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId	, a.ApplicationId)	
	AND		a.PortfolioXCustodianAccountId =
			CASE
				WHEN @PortfolioXCustodianAccountId IS NULL THEN a.PortfolioXCustodianAccountId
				ELSE @PortfolioXCustodianAccountId
			END
	AND		a.CustodianAccountId =
			CASE
				WHEN @CustodianAccountId IS NULL THEN a.CustodianAccountId
				ELSE @CustodianAccountId
			END
	AND		a.PortfolioId =
			CASE
				WHEN @PortfolioId IS NULL THEN a.PortfolioId
				ELSE @PortfolioId
			END
	ORDER BY	a.PortfolioXCustodianAccountId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE PortfolioXCustodianAccountId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.PortfolioXCustodianAccountId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.PortfolioXCustodianAccountId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.PortfolioXCustodianAccountId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.PortfolioXCustodianAccountId=b.PortfolioXCustodianAccountId
		ORDER BY	a.PortfolioXCustodianAccountId
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
		ORDER BY	a.PortfolioXCustodianAccountId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @PortfolioXCustodianAccountId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
