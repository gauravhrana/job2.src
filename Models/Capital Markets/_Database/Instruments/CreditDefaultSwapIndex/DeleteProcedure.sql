IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CreditDefaultSwapIndexDelete') 
	BEGIN
	DROP Procedure CreditDefaultSwapIndexDelete
END
GO

CREATE Procedure dbo.CreditDefaultSwapIndexDelete
(
		@CreditDefaultSwapIndexId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='CreditDefaultSwapIndex'
)
AS
BEGIN
		DELETE dbo.CreditDefaultSwapIndex
		WHERE	CreditDefaultSwapIndexId = @CreditDefaultSwapIndexId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CreditDefaultSwapIndexId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
