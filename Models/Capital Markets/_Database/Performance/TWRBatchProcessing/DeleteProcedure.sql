IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TWRBatchProcessingDelete') 
	BEGIN
	DROP Procedure TWRBatchProcessingDelete
END
GO

CREATE Procedure dbo.TWRBatchProcessingDelete
(
		@TWRBatchProcessingId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='TWRBatchProcessing'
)
AS
BEGIN
		DELETE dbo.TWRBatchProcessing
		WHERE	TWRBatchProcessingId = @TWRBatchProcessingId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TWRBatchProcessingId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
