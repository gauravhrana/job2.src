IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ManagerDelete') 
	BEGIN
	DROP Procedure ManagerDelete
END
GO

CREATE Procedure dbo.ManagerDelete
(
		@ManagerId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Manager'
)
AS
BEGIN
		DELETE dbo.Manager
		WHERE	ManagerId = @ManagerId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ManagerId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
