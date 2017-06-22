IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CaseStatusDelete') 
BEGIN
	DROP Procedure CaseStatusDelete
END
GO

CREATE Procedure dbo.CaseStatusDelete
(
		@CaseStatusId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CaseStatus'
)
AS
BEGIN

	DELETE dbo.CaseStatus
	WHERE	CaseStatusId = @CaseStatusId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CaseStatusId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
