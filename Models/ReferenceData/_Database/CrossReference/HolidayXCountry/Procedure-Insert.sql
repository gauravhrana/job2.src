IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='HolidayXCountryInsert') 
BEGIN
	DROP Procedure HolidayXCountryInsert
END
GO

CREATE Procedure dbo.HolidayXCountryInsert
(
		@HolidayXCountryId				INT		= NULL 	OUTPUT 
	,	@HolidayId				INT
	,	@CountryId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'HolidayXCountry'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @HolidayXCountryId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.HolidayXCountry
	(
			HolidayXCountryId
		,	HolidayId
		,	CountryId
		,	ApplicationId
	)
	VALUES
	(
			@HolidayXCountryId
		,	@HolidayId
		,	@CountryId
		,	@ApplicationId
	)

	SELECT @HolidayXCountryId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @HolidayXCountryId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
