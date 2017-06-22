IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StrategyDelete') 
BEGIN
	DROP Procedure StrategyDelete
END
GO

CREATE Procedure dbo.StrategyDelete
(
		@StrategyId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Strategy'
)
AS
BEGIN

	DELETE dbo.Strategy
	WHERE	StrategyId = @StrategyId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @StrategyId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
