IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CityInsert') 
BEGIN
	DROP Procedure CityInsert
END
GO

CREATE Procedure dbo.CityInsert
(
		@CityId				INT		= NULL 	OUTPUT 
	,	@CountryId				INT
	,	@Name				VARCHAR(500)
	,	@Description				VARCHAR(500)
	,	@SortOrder				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'City'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CityId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.City
	(
			CityId
		,	CountryId
		,	Name
		,	Description
		,	SortOrder
		,	ApplicationId
	)
	VALUES
	(
			@CityId
		,	@CountryId
		,	@Name
		,	@Description
		,	@SortOrder
		,	@ApplicationId
	)

	SELECT @CityId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CityId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
