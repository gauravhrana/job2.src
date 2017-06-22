IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TxInvestmentDelete') 
BEGIN
	DROP Procedure TxInvestmentDelete
END
GO

CREATE Procedure dbo.TxInvestmentDelete
(
		@TxInvestmentId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TxInvestment'
)
AS
BEGIN

	DELETE dbo.TxInvestment
	WHERE	TxInvestmentId = @TxInvestmentId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TxInvestmentId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
