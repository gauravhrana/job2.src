IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ClassInstanceInsert') 
BEGIN
	DROP Procedure ClassInstanceInsert
END
GO

CREATE Procedure dbo.ClassInstanceInsert
(
		@ClassInstanceId				INT		= NULL 	OUTPUT 
	,	@Name				VARCHAR(500)
	,	@CourseId				INT
	,	@DepartmentId				INT
	,	@TeacherId				INT
	,	@StartTime				DATETIME
	,	@EndTime				DATETIME
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'ClassInstance'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @ClassInstanceId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.ClassInstance
	(
			ClassInstanceId
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
			@ClassInstanceId
		,	@Name
		,	@CourseId
		,	@DepartmentId
		,	@TeacherId
		,	@StartTime
		,	@EndTime
		,	@ApplicationId
	)

	SELECT @ClassInstanceId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @ClassInstanceId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
