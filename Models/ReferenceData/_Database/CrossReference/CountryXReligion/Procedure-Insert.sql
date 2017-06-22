IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CountryXReligionInsert') 
BEGIN
	DROP Procedure CountryXReligionInsert
END
GO

CREATE Procedure dbo.CountryXReligionInsert
(
		@CountryXReligionId				INT		= NULL 	OUTPUT 
	,	@CountryId				INT
	,	@ReligionId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CountryXReligion'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @CountryXReligionId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.CountryXReligion
	(
			CountryXReligionId
		,	CountryId
		,	ReligionId
		,	ApplicationId
	)
	VALUES
	(
			@CountryXReligionId
		,	@CountryId
		,	@ReligionId
		,	@ApplicationId
	)

	SELECT @CountryXReligionId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @CountryXReligionId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
