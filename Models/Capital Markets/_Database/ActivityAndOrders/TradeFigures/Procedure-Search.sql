IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeFiguresSearch') 
BEGIN
	DROP Procedure TxTradeFiguresSearch
END
GO

CREATE Procedure dbo.TxTradeFiguresSearch
(
		@TxTradeFiguresId				INT		= NULL
	,	@TransactionEventId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'TxTradeFigures'
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
			a. TxTradeFiguresId
		,	a. TransactionEventId
		,	TransactionEvent.TransactionEventId AS TransactionEvent
		,	a. Quantity
		,	a. Price
		,	a. TotalCommission
		,	a. BrokerCodeId
		,	a. GlobalFacilityAmount
		,	a. ExemptUnrealizedPLfromCapitalRatios
		,	a. InternalTradeExcludeInByPassStrategy
		,	a. ForwardFXBookCurrencyPricing
		,	a. OriginalFace
		,	a. IndexRatio
		,	a. PerShareAmount
		,	a. OpeningRate
		,	a. PercentageOwned
		,	a. DelayedCompensationId
		,	a. ReceiveFinancing
		,	a. Yield
		,	a. NotionalAmount
		,	a. TradesAsId
		,	a. DirtyPrice
		,	a. TradesFlat
		,	a. RestateUnrealizedGainOrLossAtPeriodEndSpotRate
		,	a. OverridingFinancingId
		,	a. AccrueCommission
		,	a. EffectiveYield
		,	a. NetTrade
		,	a. PayOrReceiveFullCoupon
		,	a. ExpirationDate
		,	a. SweepCashOnSettlementDate
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.TxTradeFigures a
	INNER JOIN TransactionEvent ON TransactionEvent.TransactionEventId = a.TransactionEventId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.TxTradeFiguresId = ISNULL(@TxTradeFiguresId, a.TxTradeFiguresId)
	AND		a.TransactionEventId = ISNULL(@TransactionEventId, a.TransactionEventId)
	ORDER BY	a.TxTradeFiguresId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE TxTradeFiguresId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.TxTradeFiguresId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.TxTradeFiguresId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.TxTradeFiguresId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.TxTradeFiguresId=b.TxTradeFiguresId
		ORDER BY	a.TxTradeFiguresId
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
		ORDER BY	a.TxTradeFiguresId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @TxTradeFiguresId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
