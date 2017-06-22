IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TaxAccountTypeDelete') 
	BEGIN
	DROP Procedure TaxAccountTypeDelete
END
GO

CREATE Procedure dbo.TaxAccountTypeDelete
(
		@TaxAccountTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='TaxAccountType'
)
AS
BEGIN
		DELETE dbo.TaxAccountType
		WHERE	TaxAccountTypeId = @TaxAccountTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TaxAccountTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
