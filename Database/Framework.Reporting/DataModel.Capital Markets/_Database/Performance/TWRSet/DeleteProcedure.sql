IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TWRSetDelete') 
	BEGIN
	DROP Procedure TWRSetDelete
END
GO

CREATE Procedure dbo.TWRSetDelete
(
		@TWRSetId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='TWRSet'
)
AS
BEGIN
		DELETE dbo.TWRSet
		WHERE	TWRSetId = @TWRSetId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TWRSetId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
