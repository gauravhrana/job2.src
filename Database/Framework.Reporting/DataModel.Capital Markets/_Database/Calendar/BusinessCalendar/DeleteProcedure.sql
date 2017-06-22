IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BusinessCalenderDelete') 
	BEGIN
	DROP Procedure BusinessCalenderDelete
END
GO

CREATE Procedure dbo.BusinessCalenderDelete
(
		@BusinessCalenderId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='BusinessCalender'
)
AS
BEGIN
		DELETE dbo.BusinessCalender
		WHERE	BusinessCalenderId = @BusinessCalenderId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @BusinessCalenderId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
