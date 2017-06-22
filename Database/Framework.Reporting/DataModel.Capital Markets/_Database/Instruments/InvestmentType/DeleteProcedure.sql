IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='InvestmentTypeDelete') 
	BEGIN
	DROP Procedure InvestmentTypeDelete
END
GO

CREATE Procedure dbo.InvestmentTypeDelete
(
		@InvestmentTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='InvestmentType'
)
AS
BEGIN
		DELETE dbo.InvestmentType
		WHERE	InvestmentTypeId = @InvestmentTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @InvestmentTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
