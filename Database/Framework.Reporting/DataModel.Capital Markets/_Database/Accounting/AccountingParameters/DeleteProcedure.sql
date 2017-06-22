IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccountingParametersDelete') 
	BEGIN
	DROP Procedure AccountingParametersDelete
END
GO

CREATE Procedure dbo.AccountingParametersDelete
(
		@AccountingParametersId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccountingParameters'
)
AS
BEGIN
		DELETE dbo.AccountingParameters
		WHERE	AccountingParametersId = @AccountingParametersId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccountingParametersId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
