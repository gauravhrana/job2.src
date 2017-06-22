IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='DeliveryAgentDelete') 
	BEGIN
	DROP Procedure DeliveryAgentDelete
END
GO

CREATE Procedure dbo.DeliveryAgentDelete
(
		@DeliveryAgentId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='DeliveryAgent'
)
AS
BEGIN
		DELETE dbo.DeliveryAgent
		WHERE	DeliveryAgentId = @DeliveryAgentId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @DeliveryAgentId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
