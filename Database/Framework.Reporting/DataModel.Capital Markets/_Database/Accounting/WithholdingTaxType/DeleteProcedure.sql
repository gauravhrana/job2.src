IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='WithholdingTaxTypeDelete') 
	BEGIN
	DROP Procedure WithholdingTaxTypeDelete
END
GO

CREATE Procedure dbo.WithholdingTaxTypeDelete
(
		@WithholdingTaxTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='WithholdingTaxType'
)
AS
BEGIN
		DELETE dbo.WithholdingTaxType
		WHERE	WithholdingTaxTypeId = @WithholdingTaxTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @WithholdingTaxTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
