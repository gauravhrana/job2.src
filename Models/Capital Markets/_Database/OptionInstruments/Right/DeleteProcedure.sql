IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RightDelete') 
	BEGIN
	DROP Procedure RightDelete
END
GO

CREATE Procedure dbo.RightDelete
(
		@RightId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Right'
)
AS
BEGIN
		DELETE dbo.Right
		WHERE	RightId = @RightId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @RightId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
