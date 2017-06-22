IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='DiaperStatusDelete') 
	BEGIN
	DROP Procedure DiaperStatusDelete
END
GO

CREATE Procedure dbo.DiaperStatusDelete
(
		@DiaperStatusId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='DiaperStatus'
)
AS
BEGIN
		DELETE dbo.DiaperStatus
		WHERE	DiaperStatusId = @DiaperStatusId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @DiaperStatusId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
