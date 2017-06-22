IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BrokerGroupDelete') 
BEGIN
	DROP Procedure BrokerGroupDelete
END
GO

CREATE Procedure dbo.BrokerGroupDelete
(
		@BrokerGroupId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'BrokerGroup'
)
AS
BEGIN

	DELETE dbo.BrokerGroup
	WHERE		BrokerGroupId = ISNULL(@BrokerGroupId, BrokerGroupId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @BrokerGroupId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
