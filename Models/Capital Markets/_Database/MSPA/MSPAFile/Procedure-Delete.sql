IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='MSPAFileDelete') 
BEGIN
	DROP Procedure MSPAFileDelete
END
GO

CREATE Procedure dbo.MSPAFileDelete
(
		@MSPAFileId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'MSPAFile'
)
AS
BEGIN

	DELETE dbo.MSPAFile
	WHERE	MSPAFileId = @MSPAFileId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @MSPAFileId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
