IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='ReligionUpdate') 
BEGIN
	DROP Procedure ReligionUpdate
END
GO

CREATE Procedure dbo.ReligionUpdate
(
		@ReligionId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Religion'
)
AS
BEGIN

	UPDATE	dbo.Religion
	SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	ReligionId			=   @ReligionId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @ReligionId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
