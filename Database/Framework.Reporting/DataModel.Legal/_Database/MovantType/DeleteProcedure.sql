IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MovantTypeDelete') 
BEGIN
	DROP Procedure MovantTypeDelete
END
GO

CREATE Procedure dbo.MovantTypeDelete
(
		@MovantTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'MovantType'
)
AS
BEGIN

	DELETE dbo.MovantType
	WHERE	MovantTypeId = @MovantTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @MovantTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
