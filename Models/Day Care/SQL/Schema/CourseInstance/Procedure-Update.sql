IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CourseInstanceUpdate') 
BEGIN
	DROP Procedure CourseInstanceUpdate
END
GO

CREATE Procedure dbo.CourseInstanceUpdate
(
		@CourseInstanceId				INT
	,	@Name				VARCHAR(500)
	,	@CourseId				INT
	,	@DepartmentId				INT
	,	@TeacherId				INT
	,	@StartTime				DATETIME
	,	@EndTime				DATETIME
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CourseInstance'
)
AS
BEGIN

	UPDATE	dbo.CourseInstance
	SET
			Name				=	@Name
		,	CourseId				=	@CourseId
		,	DepartmentId				=	@DepartmentId
		,	TeacherId				=	@TeacherId
		,	StartTime				=	@StartTime
		,	EndTime				=	@EndTime
	WHERE	CourseInstanceId			=   @CourseInstanceId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CourseInstanceId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
