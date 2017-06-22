IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventSellSearch') 
BEGIN
	DROP Procedure TransactionEventSellSearch
END
GO

CREATE Procedure dbo.TransactionEventSellSearch
(
		@TransactionEventSellId				INT		= NULL
	,	@TransactionTypeId				INT		= NULL
	,	@CustodianId				INT		= NULL
	,	@StrategyId				INT		= NULL
	,	@AccountSpecificTypeId				INT		= NULL
	,	@InvestmentTypeId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'TransactionEventSell'
	,	@ApplicationMode				INT							= NULL
	,	@AddAuditInfo					INT							= 1
	,	@AddTraceInfo					INT							= 0
	,	@ReturnAuditInfo				INT							= 0
)
WITH RECOMPILE
AS
BEGIN


	DECLARE @SystemEntityTypeId AS INT
	SELECT @SystemEntityTypeId = dbo.GetSystemEntityTypeId(@SystemEntityType)


	SELECT 
			a. TransactionEventSellId
		,	a. TransactionEventDate
		,	a. TransactionSettleDate
		,	a. TransactionTypeId
		,	TransactionType.Name AS TransactionType
		,	a. CustodianId
		,	Custodian.Name AS Custodian
		,	a. StrategyId
		,	Strategy.Name AS Strategy
		,	a. AccountSpecificTypeId
		,	AccountSpecificType.Name AS AccountSpecificType
		,	a. InvestmentTypeId
		,	InvestmentType.Name AS InvestmentType
		,	a. Quantity
		,	a. Price
		,	a. Fees
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.TransactionEventSell a
	INNER JOIN TransactionType ON TransactionType.TransactionTypeId = a.TransactionTypeId
	INNER JOIN Custodian ON Custodian.CustodianId = a.CustodianId
	INNER JOIN Strategy ON Strategy.StrategyId = a.StrategyId
	INNER JOIN AccountSpecificType ON AccountSpecificType.AccountSpecificTypeId = a.AccountSpecificTypeId
	INNER JOIN InvestmentType ON InvestmentType.InvestmentTypeId = a.InvestmentTypeId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.TransactionEventSellId = ISNULL(@TransactionEventSellId, a.TransactionEventSellId)
	AND		a.TransactionTypeId = ISNULL(@TransactionTypeId, a.TransactionTypeId)
	AND		a.CustodianId = ISNULL(@CustodianId, a.CustodianId)
	AND		a.StrategyId = ISNULL(@StrategyId, a.StrategyId)
	AND		a.AccountSpecificTypeId = ISNULL(@AccountSpecificTypeId, a.AccountSpecificTypeId)
	AND		a.InvestmentTypeId = ISNULL(@InvestmentTypeId, a.InvestmentTypeId)
	ORDER BY	a.TransactionEventSellId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE TransactionEventSellId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.TransactionEventSellId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.TransactionEventSellId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.TransactionEventSellId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.TransactionEventSellId=b.TransactionEventSellId
		ORDER BY	a.TransactionEventSellId
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
		ORDER BY	a.TransactionEventSellId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @TransactionEventSellId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
