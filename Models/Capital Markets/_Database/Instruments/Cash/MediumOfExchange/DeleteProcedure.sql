IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MediumOfExchangeDelete') 
	BEGIN
	DROP Procedure MediumOfExchangeDelete
END
GO

CREATE Procedure dbo.MediumOfExchangeDelete
(
		@MediumOfExchangeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='MediumOfExchange'
)
AS
BEGIN
		DELETE dbo.MediumOfExchange
		WHERE	MediumOfExchangeId = @MediumOfExchangeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MediumOfExchangeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
