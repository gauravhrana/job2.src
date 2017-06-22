IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ClassInstanceDelete') 
BEGIN
	DROP Procedure ClassInstanceDelete
END
GO

CREATE Procedure dbo.ClassInstanceDelete
(
		@ClassInstanceId				INT		= NULL
	,	@CourseId				INT		= NULL
	,	@DepartmentId				INT		= NULL
	,	@TeacherId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'ClassInstance'
)
AS
BEGIN

	DELETE dbo.ClassInstance
	WHERE		ClassInstanceId = ISNULL(@ClassInstanceId, ClassInstanceId)
	AND			CourseId = ISNULL(@CourseId, CourseId)
	AND			DepartmentId = ISNULL(@DepartmentId, DepartmentId)
	AND			TeacherId = ISNULL(@TeacherId, TeacherId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @ClassInstanceId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
