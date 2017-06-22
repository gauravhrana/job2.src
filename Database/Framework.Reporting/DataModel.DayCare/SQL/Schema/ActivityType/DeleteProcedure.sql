IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ActivityTypeDelete') 
	BEGIN
	DROP Procedure ActivityTypeDelete
END
GO

CREATE Procedure dbo.ActivityTypeDelete
(
		@ActivityTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='ActivityType'
)
AS
BEGIN
		DELETE dbo.ActivityType
		WHERE	ActivityTypeId = @ActivityTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ActivityTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
