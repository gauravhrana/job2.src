IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CourseInstanceInsert') 
BEGIN
	DROP Procedure CourseInstanceInsert
END
GO

CREATE Procedure dbo.CourseInstanceInsert
(
		@CourseInstanceId				INT		= NULL 	OUTPUT 
	,	@Name				VARCHAR(500)
	,	@CourseId				INT
	,	@DepartmentId				INT
	,	@TeacherId				INT
	,	@StartTime				DATETIME
	,	@EndTime				DATETIME
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CourseInstance'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CourseInstanceId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.CourseInstance
	(
			CourseInstanceId
		,	Name
		,	CourseId
		,	DepartmentId
		,	TeacherId
		,	StartTime
		,	EndTime
		,	ApplicationId
	)
	VALUES
	(
			@CourseInstanceId
		,	@Name
		,	@CourseId
		,	@DepartmentId
		,	@TeacherId
		,	@StartTime
		,	@EndTime
		,	@ApplicationId
	)

	SELECT @CourseInstanceId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CourseInstanceId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
