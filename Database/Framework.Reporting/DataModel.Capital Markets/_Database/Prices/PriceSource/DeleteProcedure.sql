IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceSourceDelete') 
	BEGIN
	DROP Procedure PriceSourceDelete
END
GO

CREATE Procedure dbo.PriceSourceDelete
(
		@PriceSourceId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PriceSource'
)
AS
BEGIN
		DELETE dbo.PriceSource
		WHERE	PriceSourceId = @PriceSourceId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PriceSourceId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
