IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RecordTypeDelete') 
BEGIN
	DROP Procedure RecordTypeDelete
END
GO

CREATE Procedure dbo.RecordTypeDelete
(
		@RecordTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'RecordType'
)
AS
BEGIN

	DELETE dbo.RecordType
	WHERE	RecordTypeId = @RecordTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @RecordTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
