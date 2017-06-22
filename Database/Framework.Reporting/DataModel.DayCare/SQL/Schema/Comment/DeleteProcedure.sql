IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CommentDelete') 
	BEGIN
	DROP Procedure CommentDelete
END
GO

CREATE Procedure dbo.CommentDelete
(
		@CommentId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Comment'
)
AS
BEGIN
		DELETE dbo.Comment
		WHERE	CommentId = @CommentId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CommentId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
