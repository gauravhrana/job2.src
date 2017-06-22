IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CourseInstanceDelete') 
BEGIN
	DROP Procedure CourseInstanceDelete
END
GO

CREATE Procedure dbo.CourseInstanceDelete
(
		@CourseInstanceId				INT		= NULL
	,	@CourseId				INT		= NULL
	,	@DepartmentId				INT		= NULL
	,	@TeacherId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CourseInstance'
)
AS
BEGIN

	DELETE dbo.CourseInstance
	WHERE		CourseInstanceId = ISNULL(@CourseInstanceId, CourseInstanceId)
	AND			CourseId = ISNULL(@CourseId, CourseId)
	AND			DepartmentId = ISNULL(@DepartmentId, DepartmentId)
	AND			TeacherId = ISNULL(@TeacherId, TeacherId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CourseInstanceId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
