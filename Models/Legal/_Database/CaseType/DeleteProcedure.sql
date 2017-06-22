IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CaseTypeDelete') 
BEGIN
	DROP Procedure CaseTypeDelete
END
GO

CREATE Procedure dbo.CaseTypeDelete
(
		@CaseTypeId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CaseType'
)
AS
BEGIN

	DELETE dbo.CaseType
	WHERE	CaseTypeId = @CaseTypeId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CaseTypeId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
