IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='RegistrationDelete') 
BEGIN
	DROP Procedure RegistrationDelete
END
GO

CREATE Procedure dbo.RegistrationDelete
(
		@RegistrationId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'Registration'
)
AS
BEGIN

	DELETE dbo.Registration
	WHERE	RegistrationId = @RegistrationId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @RegistrationId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
