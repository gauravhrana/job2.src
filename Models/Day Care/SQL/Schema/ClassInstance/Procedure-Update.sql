IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ClassInstanceUpdate') 
BEGIN
	DROP Procedure ClassInstanceUpdate
END
GO

CREATE Procedure dbo.ClassInstanceUpdate
(
		@ClassInstanceId				INT
	,	@Name				VARCHAR(500)
	,	@CourseId				INT
	,	@DepartmentId				INT
	,	@TeacherId				INT
	,	@StartTime				DATETIME
	,	@EndTime				DATETIME
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'ClassInstance'
)
AS
BEGIN

	UPDATE	dbo.ClassInstance
	SET
			Name				=	@Name
		,	CourseId				=	@CourseId
		,	DepartmentId				=	@DepartmentId
		,	TeacherId				=	@TeacherId
		,	StartTime				=	@StartTime
		,	EndTime				=	@EndTime
	WHERE	ClassInstanceId			=   @ClassInstanceId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ClassInstanceId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
