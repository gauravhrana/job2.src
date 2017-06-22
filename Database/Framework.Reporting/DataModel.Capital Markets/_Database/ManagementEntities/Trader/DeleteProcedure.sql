IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TraderDelete') 
BEGIN
	DROP Procedure TraderDelete
END
GO

CREATE Procedure dbo.TraderDelete
(
		@TraderId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Trader'
)
AS
BEGIN

	DELETE dbo.Trader
	WHERE	TraderId = @TraderId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TraderId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
