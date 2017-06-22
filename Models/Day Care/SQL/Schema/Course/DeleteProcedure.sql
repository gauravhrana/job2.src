IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CourseDelete') 
	BEGIN
	DROP Procedure CourseDelete
END
GO

CREATE Procedure dbo.CourseDelete
(
		@CourseId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50)='Course'
)
AS
BEGIN
		DELETE dbo.Course
		WHERE	CourseId = @CourseId

		EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CourseId
		,	@AuditAction		= 'Delete'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

	END
GO
