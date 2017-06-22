IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AutomatedFreezepointDelete') 
	BEGIN
	DROP Procedure AutomatedFreezepointDelete
END
GO

CREATE Procedure dbo.AutomatedFreezepointDelete
(
		@AutomatedFreezepointId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='AutomatedFreezepoint'
)
AS
BEGIN
		DELETE dbo.AutomatedFreezepoint
		WHERE	AutomatedFreezepointId = @AutomatedFreezepointId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AutomatedFreezepointId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
