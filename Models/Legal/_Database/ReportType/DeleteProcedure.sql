IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ReportTypeDelete') 
BEGIN
	DROP Procedure ReportTypeDelete
END
GO

CREATE Procedure dbo.ReportTypeDelete
(
		@ReportTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'ReportType'
)
AS
BEGIN

	DELETE dbo.ReportType
	WHERE	ReportTypeId = @ReportTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @ReportTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
