IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CountryUpdate') 
BEGIN
	DROP Procedure CountryUpdate
END
GO

CREATE Procedure dbo.CountryUpdate
(
		@CountryId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Country'
)
AS
BEGIN

	UPDATE	dbo.Country
	SET
			Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	CountryId			=   @CountryId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CountryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
