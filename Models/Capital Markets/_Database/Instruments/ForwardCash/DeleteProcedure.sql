IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ForwardCashDelete') 
	BEGIN
	DROP Procedure ForwardCashDelete
END
GO

CREATE Procedure dbo.ForwardCashDelete
(
		@ForwardCashId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='ForwardCash'
)
AS
BEGIN
		DELETE dbo.ForwardCash
		WHERE	ForwardCashId = @ForwardCashId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ForwardCashId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
