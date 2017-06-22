IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CalendarDelete') 
	BEGIN
	DROP Procedure CalendarDelete
END
GO

CREATE Procedure dbo.CalendarDelete
(
		@CalendarId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Calendar'
)
AS
BEGIN
		DELETE dbo.Calendar
		WHERE	CalendarId = @CalendarId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CalendarId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
