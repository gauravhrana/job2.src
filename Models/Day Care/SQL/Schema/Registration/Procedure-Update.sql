IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RegistrationUpdate') 
BEGIN
	DROP Procedure RegistrationUpdate
END
GO

CREATE Procedure dbo.RegistrationUpdate
(
		@RegistrationId				INT
	,	@CourseId				INT
	,	@StudentId				INT
	,	@EnrollmentDate				DATETIME
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Registration'
)
AS
BEGIN

	UPDATE	dbo.Registration
	SET
			CourseId				=	@CourseId
		,	StudentId				=	@StudentId
		,	EnrollmentDate				=	@EnrollmentDate
	WHERE	RegistrationId			=   @RegistrationId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @RegistrationId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
