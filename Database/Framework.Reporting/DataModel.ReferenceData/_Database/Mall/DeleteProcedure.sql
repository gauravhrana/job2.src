IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MallDelete') 
	BEGIN
	DROP Procedure MallDelete
END
GO

CREATE Procedure dbo.MallDelete
(
		@MallId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Mall'
)
AS
BEGIN
		DELETE dbo.Mall
		WHERE	MallId = @MallId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @MallId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
