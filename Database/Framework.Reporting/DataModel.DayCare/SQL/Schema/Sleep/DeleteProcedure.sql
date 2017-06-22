IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SleepDelete') 
	BEGIN
	DROP Procedure SleepDelete
END
GO

CREATE Procedure dbo.SleepDelete
(
		@SleepId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Sleep'
)
AS
BEGIN
		DELETE dbo.Sleep
		WHERE	SleepId = @SleepId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SleepId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
