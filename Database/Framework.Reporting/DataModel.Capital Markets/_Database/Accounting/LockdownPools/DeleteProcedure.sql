IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='LockdownPoolsDelete') 
	BEGIN
	DROP Procedure LockdownPoolsDelete
END
GO

CREATE Procedure dbo.LockdownPoolsDelete
(
		@LockdownPoolsId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='LockdownPools'
)
AS
BEGIN
		DELETE dbo.LockdownPools
		WHERE	LockdownPoolsId = @LockdownPoolsId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @LockdownPoolsId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
