IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TimeZoneXCountryInsert') 
BEGIN
	DROP Procedure TimeZoneXCountryInsert
END
GO

CREATE Procedure dbo.TimeZoneXCountryInsert
(
		@TimeZoneXCountryId				INT		= NULL 	OUTPUT 
	,	@TimeZoneId				INT
	,	@CountryId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TimeZoneXCountry'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @TimeZoneXCountryId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.TimeZoneXCountry
	(
			TimeZoneXCountryId
		,	TimeZoneId
		,	CountryId
		,	ApplicationId
	)
	VALUES
	(
			@TimeZoneXCountryId
		,	@TimeZoneId
		,	@CountryId
		,	@ApplicationId
	)

	SELECT @TimeZoneXCountryId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @TimeZoneXCountryId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
