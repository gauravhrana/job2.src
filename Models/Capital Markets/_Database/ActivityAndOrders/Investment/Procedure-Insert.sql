IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxInvestmentInsert') 
BEGIN
	DROP Procedure TxInvestmentInsert
END
GO

CREATE Procedure dbo.TxInvestmentInsert
(
		@TxInvestmentId				INT		= NULL 	OUTPUT 
	,	@TransactionEventId				INT
	,	@InvestmentId				INT
	,	@CustAccountId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TxInvestment'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TxInvestmentId Output, @AuditId


	INSERT INTO dbo.TxInvestment
	(
			TxInvestmentId
		,	TransactionEventId
		,	InvestmentId
		,	CustAccountId
		,	ApplicationId
	)
	VALUES
	(
			@TxInvestmentId
		,	@TransactionEventId
		,	@InvestmentId
		,	@CustAccountId
		,	@ApplicationId
	)

	SELECT @TxInvestmentId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TxInvestmentId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
