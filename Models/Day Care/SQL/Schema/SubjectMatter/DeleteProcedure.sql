IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SubjectMatterDelete') 
	BEGIN
	DROP Procedure SubjectMatterDelete
END
GO

CREATE Procedure dbo.SubjectMatterDelete
(
		@SubjectMatterId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='SubjectMatter'
)
AS
BEGIN
		DELETE dbo.SubjectMatter
		WHERE	SubjectMatterId = @SubjectMatterId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SubjectMatterId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
