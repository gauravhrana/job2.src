IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxTradeAndSettleDatesSearch') 
BEGIN
	DROP Procedure TxTradeAndSettleDatesSearch
END
GO

CREATE Procedure dbo.TxTradeAndSettleDatesSearch
(
		@TxTradeAndSettleDatesId				INT		= NULL
	,	@TransactionEventId				INT		= NULL
	,	@FromSearchTradeDate				DATETIME = NULL
	,	@ToSearchTradeDate				DATETIME = NULL
	,	@FromSearchContractualDate				DATETIME = NULL
	,	@ToSearchContractualDate				DATETIME = NULL
	,	@FromSearchActualDate				DATETIME = NULL
	,	@ToSearchActualDate				DATETIME = NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'TxTradeAndSettleDates'
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
			a. TxTradeAndSettleDatesId
		,	a. TransactionEventId
		,	TransactionEvent.TransactionEventId AS TransactionEvent
		,	a. TradeDate
		,	a. ContractualDate
		,	a. ActualDate
		,	a. SpotDate
		,	a. SettlementDate
		,	a. ApplicationId
	INTO		#TempMain
	FROM		dbo.TxTradeAndSettleDates a
	INNER JOIN TransactionEvent ON TransactionEvent.TransactionEventId = a.TransactionEventId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.TxTradeAndSettleDatesId = ISNULL(@TxTradeAndSettleDatesId, a.TxTradeAndSettleDatesId)
	AND		a.TransactionEventId = ISNULL(@TransactionEventId, a.TransactionEventId)
	AND		a.TradeDate BETWEEN COALESCE(@FromSearchTradeDate, a.TradeDate) 	AND	 COALESCE(@ToSearchTradeDate, a.TradeDate)
	AND		a.ContractualDate BETWEEN COALESCE(@FromSearchContractualDate, a.ContractualDate) 	AND	 COALESCE(@ToSearchContractualDate, a.ContractualDate)
	AND		a.ActualDate BETWEEN COALESCE(@FromSearchActualDate, a.ActualDate) 	AND	 COALESCE(@ToSearchActualDate, a.ActualDate)
	ORDER BY	a.TxTradeAndSettleDatesId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE TxTradeAndSettleDatesId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.TxTradeAndSettleDatesId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.TxTradeAndSettleDatesId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.TxTradeAndSettleDatesId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.TxTradeAndSettleDatesId=b.TxTradeAndSettleDatesId
		ORDER BY	a.TxTradeAndSettleDatesId
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
		ORDER BY	a.TxTradeAndSettleDatesId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @TxTradeAndSettleDatesId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
