IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TeacherDelete') 
	BEGIN
	DROP Procedure TeacherDelete
END
GO

CREATE Procedure dbo.TeacherDelete
(
		@TeacherId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Teacher'
)
AS
BEGIN
		DELETE dbo.Teacher
		WHERE	TeacherId = @TeacherId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TeacherId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
