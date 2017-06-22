IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TransactionEventCoverShortSearch') 
BEGIN
	DROP Procedure TransactionEventCoverShortSearch
END
GO

CREATE Procedure dbo.TransactionEventCoverShortSearch
(
		@TransactionEventCoverShortId				INT		= NULL
	,	@TransactionTypeId				INT		= NULL
	,	@CustodianId				INT		= NULL
	,	@StrategyId				INT		= NULL
	,	@AccountSpecificTypeId				INT		= NULL
	,	@InvestmentTypeId				INT		= NULL
	,	@ApplicationId						INT	=	 NULL
	,	@AuditId						INT
	,	@AuditDate						DATETIME 					= NULL
	,	@SystemEntityType				VARCHAR(50)					= 'TransactionEventCoverShort'
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
			a. TransactionEventCoverShortId
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
	FROM		dbo.TransactionEventCoverShort a
	INNER JOIN TransactionType ON TransactionType.TransactionTypeId = a.TransactionTypeId
	INNER JOIN Custodian ON Custodian.CustodianId = a.CustodianId
	INNER JOIN Strategy ON Strategy.StrategyId = a.StrategyId
	INNER JOIN AccountSpecificType ON AccountSpecificType.AccountSpecificTypeId = a.AccountSpecificTypeId
	INNER JOIN InvestmentType ON InvestmentType.InvestmentTypeId = a.InvestmentTypeId
	WHERE	a.ApplicationId = ISNULL(@ApplicationId, a.ApplicationId)
	AND		a.TransactionEventCoverShortId = ISNULL(@TransactionEventCoverShortId, a.TransactionEventCoverShortId)
	AND		a.TransactionTypeId = ISNULL(@TransactionTypeId, a.TransactionTypeId)
	AND		a.CustodianId = ISNULL(@CustodianId, a.CustodianId)
	AND		a.StrategyId = ISNULL(@StrategyId, a.StrategyId)
	AND		a.AccountSpecificTypeId = ISNULL(@AccountSpecificTypeId, a.AccountSpecificTypeId)
	AND		a.InvestmentTypeId = ISNULL(@InvestmentTypeId, a.InvestmentTypeId)
	ORDER BY	a.TransactionEventCoverShortId ASC

	IF	@ApplicationMode = 1 
	BEGIN
		DELETE FROM #TempMain
		WHERE TransactionEventCoverShortId < 0
	END

	IF @ReturnAuditInfo = 1
	BEGIN

		-- get Audit latest record matching on key, systementitytype
		SELECT	c.EntityKey
			,	MAX(c.AuditHistoryId)	AS 'MaxAuditHistoryId'
		INTO		#HistortyInfo
		FROM 		#TempMain a	
		INNER JOIN	CommonServices.dbo.AuditHistory c ON	c.EntityKey			= a.TransactionEventCoverShortId
		AND		c.SystemEntityId	= @SystemEntityTypeId
		AND		c.AuditActionId		IN (1,2)
		GROUP BY	c.EntityKey	

		-- Get Audit Date and CreatedByPersonId for given records
		SELECT	a.TransactionEventCoverShortId
			,	c.AuditActionId 
			,	c.CreatedDate
			,	c.CreatedByPersonId
			, 	c.CreatedDate					AS	'UpdatedDate'
			,	e.FirstName + ' ' + e.LastName	AS	'UpdatedBy'
			,	d.Name							AS	'LastAction'
		INTO		#HistortyInfoDetails
		FROM		#TempMain a
		INNER JOIN	#HistortyInfo							b ON	b.EntityKey			= a.TransactionEventCoverShortId
		INNER JOIN	CommonServices.dbo.AuditHistory			c ON	c.AuditHistoryId	= b.MaxAuditHistoryId
		INNER JOIN	CommonServices.dbo.AuditAction			d ON	c.AuditActionId 	= d.AuditActionId
		INNER JOIN	AuthenticationAndAuthorization.dbo.ApplicationUser	e ON	c.CreatedByPersonId	= e.ApplicationUserId

		-- Show full details
		SELECT	a.*
			,	b.UpdatedDate
			,	b.UpdatedBy
			,	b.LastAction
		FROM	#TempMain		a
		LEFT JOIN	#HistortyInfoDetails	b ON	a.TransactionEventCoverShortId=b.TransactionEventCoverShortId
		ORDER BY	a.TransactionEventCoverShortId
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
		ORDER BY	a.TransactionEventCoverShortId
	END

	IF @AddAuditInfo = 1 
	BEGIN
		-- Create Audit Record
		EXEC dbo.AuditHistoryInsert 
				@SystemEntityType	= @SystemEntityType
			,	@EntityKey			= @TransactionEventCoverShortId
			,	@AuditAction		= 'Search'
			,	@CreatedDate		= @AuditDate
			,	@CreatedByPersonId	= @AuditId
	END

END
GO
