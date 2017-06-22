IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CityUpdate') 
BEGIN
	DROP Procedure CityUpdate
END
GO

CREATE Procedure dbo.CityUpdate
(
		@CityId				INT
	,	@CountryId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'City'
)
AS
BEGIN

	UPDATE	dbo.City
	SET
			CountryId				=	@CountryId
		,	Name				=	@Name
		,	Description				=	@Description
		,	SortOrder				=	@SortOrder
	WHERE	CityId			=   @CityId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CityId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
