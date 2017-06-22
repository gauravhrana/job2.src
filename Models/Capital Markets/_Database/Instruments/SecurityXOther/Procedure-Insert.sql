IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXOtherInsert') 
BEGIN
	DROP Procedure SecurityXOtherInsert
END
GO

CREATE Procedure dbo.SecurityXOtherInsert
(
		@SecurityXOtherId				INT		= NULL 	OUTPUT 
	,	@SourcedFromThomsonReuters				VARCHAR(500)
	,	@WhenIssued				VARCHAR(500)
	,	@SecurityId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXOther'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SecurityXOtherId Output, @AuditId


	INSERT INTO dbo.SecurityXOther
	(
			SecurityXOtherId
		,	SourcedFromThomsonReuters
		,	WhenIssued
		,	SecurityId
		,	ApplicationId
	)
	VALUES
	(
			@SecurityXOtherId
		,	@SourcedFromThomsonReuters
		,	@WhenIssued
		,	@SecurityId
		,	@ApplicationId
	)

	SELECT @SecurityXOtherId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SecurityXOtherId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
