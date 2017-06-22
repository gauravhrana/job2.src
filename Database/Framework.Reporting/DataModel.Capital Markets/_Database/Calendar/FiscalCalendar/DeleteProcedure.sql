IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FiscalCalenderDelete') 
	BEGIN
	DROP Procedure FiscalCalenderDelete
END
GO

CREATE Procedure dbo.FiscalCalenderDelete
(
		@FiscalCalenderId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='FiscalCalender'
)
AS
BEGIN
		DELETE dbo.FiscalCalender
		WHERE	FiscalCalenderId = @FiscalCalenderId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FiscalCalenderId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
