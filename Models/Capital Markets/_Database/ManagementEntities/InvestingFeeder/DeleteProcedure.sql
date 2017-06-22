IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='InvestingFeederDelete') 
BEGIN
	DROP Procedure InvestingFeederDelete
END
GO

CREATE Procedure dbo.InvestingFeederDelete
(
		@InvestingFeederId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'InvestingFeeder'
)
AS
BEGIN

	DELETE dbo.InvestingFeeder
	WHERE	InvestingFeederId = @InvestingFeederId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @InvestingFeederId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
