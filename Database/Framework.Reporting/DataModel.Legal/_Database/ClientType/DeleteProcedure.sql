IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ClientTypeDelete') 
BEGIN
	DROP Procedure ClientTypeDelete
END
GO

CREATE Procedure dbo.ClientTypeDelete
(
		@ClientTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'ClientType'
)
AS
BEGIN

	DELETE dbo.ClientType
	WHERE	ClientTypeId = @ClientTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @ClientTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
