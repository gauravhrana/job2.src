IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CounselDelete') 
BEGIN
	DROP Procedure CounselDelete
END
GO

CREATE Procedure dbo.CounselDelete
(
		@CounselId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Counsel'
)
AS
BEGIN

	DELETE dbo.Counsel
	WHERE	CounselId = @CounselId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CounselId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
