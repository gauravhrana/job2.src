IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FinancingDelete') 
	BEGIN
	DROP Procedure FinancingDelete
END
GO

CREATE Procedure dbo.FinancingDelete
(
		@FinancingId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Financing'
)
AS
BEGIN
		DELETE dbo.Financing
		WHERE	FinancingId = @FinancingId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FinancingId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
