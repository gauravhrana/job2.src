IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='BrokerTypeDelete') 
BEGIN
	DROP Procedure BrokerTypeDelete
END
GO

CREATE Procedure dbo.BrokerTypeDelete
(
		@BrokerTypeId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'BrokerType'
)
AS
BEGIN

	DELETE dbo.BrokerType

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @BrokerTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
