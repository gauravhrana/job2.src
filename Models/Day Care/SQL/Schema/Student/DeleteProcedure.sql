IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StudentDelete') 
	BEGIN
	DROP Procedure StudentDelete
END
GO

CREATE Procedure dbo.StudentDelete
(
		@StudentId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Student'
)
AS
BEGIN
		DELETE dbo.Student
		WHERE	StudentId = @StudentId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @StudentId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
