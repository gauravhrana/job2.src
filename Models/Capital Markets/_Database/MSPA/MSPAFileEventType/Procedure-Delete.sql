IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileEventTypeDelete') 
BEGIN
	DROP Procedure MSPAFileEventTypeDelete
END
GO

CREATE Procedure dbo.MSPAFileEventTypeDelete
(
		@MSPAFileEventTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'MSPAFileEventType'
)
AS
BEGIN

	DELETE dbo.MSPAFileEventType
	WHERE	MSPAFileEventTypeId = @MSPAFileEventTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @MSPAFileEventTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
