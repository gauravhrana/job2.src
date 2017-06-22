IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AccidentReportDelete') 
	BEGIN
	DROP Procedure AccidentReportDelete
END
GO

CREATE Procedure dbo.AccidentReportDelete
(
		@AccidentReportId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AccidentReport'
)
AS
BEGIN
		DELETE dbo.AccidentReport
		WHERE	AccidentReportId = @AccidentReportId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AccidentReportId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
