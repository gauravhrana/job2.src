IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccountingViewDelete') 
	BEGIN
	DROP Procedure AccountingViewDelete
END
GO

CREATE Procedure dbo.AccountingViewDelete
(
		@AccountingViewId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccountingView'
)
AS
BEGIN
		DELETE dbo.AccountingView
		WHERE	AccountingViewId = @AccountingViewId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccountingViewId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
