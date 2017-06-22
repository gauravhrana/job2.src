IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXOtherUpdate') 
BEGIN
	DROP Procedure SecurityXOtherUpdate
END
GO

CREATE Procedure dbo.SecurityXOtherUpdate
(
		@SecurityXOtherId				INT
	,	@SourcedFromThomsonReuters				VARCHAR(500)
	,	@WhenIssued				VARCHAR(500)
	,	@SecurityId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXOther'
)
AS
BEGIN
			SourcedFromThomsonReuters				=	@SourcedFromThomsonReuters
		,	WhenIssued				=	@WhenIssued
		,	SecurityId				=	@SecurityId
	WHERE	SecurityXOtherId			=   @SecurityXOtherId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SecurityXOtherId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
