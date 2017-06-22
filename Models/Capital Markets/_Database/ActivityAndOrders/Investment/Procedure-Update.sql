IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxInvestmentUpdate') 
BEGIN
	DROP Procedure TxInvestmentUpdate
END
GO

CREATE Procedure dbo.TxInvestmentUpdate
(
		@TxInvestmentId				INT
	,	@TransactionEventId				INT
	,	@InvestmentId				INT
	,	@CustAccountId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxInvestment'
)
AS
BEGIN
			TransactionEventId				=	@TransactionEventId
		,	InvestmentId				=	@InvestmentId
		,	CustAccountId				=	@CustAccountId
	WHERE	TxInvestmentId			=   @TxInvestmentId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TxInvestmentId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
