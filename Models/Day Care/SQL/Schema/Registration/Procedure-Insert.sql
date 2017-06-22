IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RegistrationInsert') 
BEGIN
	DROP Procedure RegistrationInsert
END
GO

CREATE Procedure dbo.RegistrationInsert
(
		@RegistrationId				INT		= NULL 	OUTPUT 
	,	@CourseId				INT
	,	@StudentId				INT
	,	@EnrollmentDate				DATETIME
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Registration'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @RegistrationId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.Registration
	(
			RegistrationId
		,	CourseId
		,	StudentId
		,	EnrollmentDate
		,	ApplicationId
	)
	VALUES
	(
			@RegistrationId
		,	@CourseId
		,	@StudentId
		,	@EnrollmentDate
		,	@ApplicationId
	)

	SELECT @RegistrationId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @RegistrationId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
