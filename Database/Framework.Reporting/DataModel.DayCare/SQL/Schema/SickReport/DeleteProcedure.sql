IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SickReportDelete') 
	BEGIN
	DROP Procedure SickReportDelete
END
GO

CREATE Procedure dbo.SickReportDelete
(
		@SickReportId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='SickReport'
)
AS
BEGIN
		DELETE dbo.SickReport
		WHERE	SickReportId = @SickReportId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SickReportId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
