IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccountingCalenderDelete') 
	BEGIN
	DROP Procedure AccountingCalenderDelete
END
GO

CREATE Procedure dbo.AccountingCalenderDelete
(
		@AccountingCalenderId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccountingCalender'
)
AS
BEGIN
		DELETE dbo.AccountingCalender
		WHERE	AccountingCalenderId = @AccountingCalenderId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccountingCalenderId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
