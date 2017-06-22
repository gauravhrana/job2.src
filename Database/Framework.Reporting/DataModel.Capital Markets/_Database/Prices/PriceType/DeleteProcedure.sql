IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PriceTypeDelete') 
	BEGIN
	DROP Procedure PriceTypeDelete
END
GO

CREATE Procedure dbo.PriceTypeDelete
(
		@PriceTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PriceType'
)
AS
BEGIN
		DELETE dbo.PriceType
		WHERE	PriceTypeId = @PriceTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PriceTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
