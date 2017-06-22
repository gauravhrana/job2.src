IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ActivitySubTypeDelete') 
	BEGIN
	DROP Procedure ActivitySubTypeDelete
END
GO

CREATE Procedure dbo.ActivitySubTypeDelete
(
		@ActivitySubTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='ActivitySubType'
)
AS
BEGIN
		DELETE dbo.ActivitySubType
		WHERE	ActivitySubTypeId = @ActivitySubTypeId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ActivitySubTypeId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
