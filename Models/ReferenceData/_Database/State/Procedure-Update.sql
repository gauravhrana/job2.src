IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='StateUpdate') 
BEGIN
	DROP Procedure StateUpdate
END
GO

CREATE Procedure dbo.StateUpdate
(
		@StateId				INT
	,	@CountryId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'State'
)
AS
BEGIN

	UPDATE	dbo.State
	SET
			CountryId				=	@CountryId
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	StateId			=   @StateId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @StateId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
