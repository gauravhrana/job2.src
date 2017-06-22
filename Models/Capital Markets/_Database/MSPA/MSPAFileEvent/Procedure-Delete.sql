IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileEventDelete') 
BEGIN
	DROP Procedure MSPAFileEventDelete
END
GO

CREATE Procedure dbo.MSPAFileEventDelete
(
		@MSPAFileEventId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'MSPAFileEvent'
)
AS
BEGIN

	DELETE dbo.MSPAFileEvent
	WHERE	MSPAFileEventId = @MSPAFileEventId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @MSPAFileEventId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
