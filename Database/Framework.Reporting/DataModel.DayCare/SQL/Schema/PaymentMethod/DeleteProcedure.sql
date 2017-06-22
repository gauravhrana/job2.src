IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PaymentMethodDelete') 
	BEGIN
	DROP Procedure PaymentMethodDelete
END
GO

CREATE Procedure dbo.PaymentMethodDelete
(
		@PaymentMethodId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='PaymentMethod'
)
AS
BEGIN
		DELETE dbo.PaymentMethod
		WHERE	PaymentMethodId = @PaymentMethodId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @PaymentMethodId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
