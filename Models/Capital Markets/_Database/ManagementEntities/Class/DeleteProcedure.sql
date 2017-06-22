IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ClassDelete') 
BEGIN
	DROP Procedure ClassDelete
END
GO

CREATE Procedure dbo.ClassDelete
(
		@ClassId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Class'
)
AS
BEGIN

	DELETE dbo.Class
	WHERE	ClassId = @ClassId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @ClassId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
