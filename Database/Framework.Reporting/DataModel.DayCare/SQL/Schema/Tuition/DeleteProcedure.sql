IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TuitionDelete') 
	BEGIN
	DROP Procedure TuitionDelete
END
GO

CREATE Procedure dbo.TuitionDelete
(
		@TuitionId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Tuition'
)
AS
BEGIN
		DELETE dbo.Tuition
		WHERE	TuitionId = @TuitionId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TuitionId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
