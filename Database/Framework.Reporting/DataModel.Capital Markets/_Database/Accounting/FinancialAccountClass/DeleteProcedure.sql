IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FinancialAccountClassDelete') 
	BEGIN
	DROP Procedure FinancialAccountClassDelete
END
GO

CREATE Procedure dbo.FinancialAccountClassDelete
(
		@FinancialAccountClassId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='FinancialAccountClass'
)
AS
BEGIN
		DELETE dbo.FinancialAccountClass
		WHERE	FinancialAccountClassId = @FinancialAccountClassId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FinancialAccountClassId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
