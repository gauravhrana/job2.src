IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TaxStatusDelete') 
	BEGIN
	DROP Procedure TaxStatusDelete
END
GO

CREATE Procedure dbo.TaxStatusDelete
(
		@TaxStatusId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='TaxStatus'
)
AS
BEGIN
		DELETE dbo.TaxStatus
		WHERE	TaxStatusId = @TaxStatusId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TaxStatusId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
