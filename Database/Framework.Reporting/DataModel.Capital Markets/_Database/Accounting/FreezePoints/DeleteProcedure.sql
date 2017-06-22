IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='FreezePointsDelete') 
	BEGIN
	DROP Procedure FreezePointsDelete
END
GO

CREATE Procedure dbo.FreezePointsDelete
(
		@FreezePointsId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='FreezePoints'
)
AS
BEGIN
		DELETE dbo.FreezePoints
		WHERE	FreezePointsId = @FreezePointsId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @FreezePointsId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
