IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='PressReleaseTypeDelete') 
BEGIN
	DROP Procedure PressReleaseTypeDelete
END
GO

CREATE Procedure dbo.PressReleaseTypeDelete
(
		@PressReleaseTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'PressReleaseType'
)
AS
BEGIN

	DELETE dbo.PressReleaseType
	WHERE	PressReleaseTypeId = @PressReleaseTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @PressReleaseTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
