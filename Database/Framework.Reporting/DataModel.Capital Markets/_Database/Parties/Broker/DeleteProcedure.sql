IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BrokerDelete') 
	BEGIN
	DROP Procedure BrokerDelete
END
GO

CREATE Procedure dbo.BrokerDelete
(
		@BrokerId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Broker'
)
AS
BEGIN
		DELETE dbo.Broker
		WHERE	BrokerId = @BrokerId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @BrokerId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
