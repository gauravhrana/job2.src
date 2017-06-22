IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='HolidayXCountryDelete') 
BEGIN
	DROP Procedure HolidayXCountryDelete
END
GO

CREATE Procedure dbo.HolidayXCountryDelete
(
		@HolidayXCountryId				INT		= NULL
	,	@HolidayId				INT		= NULL
	,	@CountryId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'HolidayXCountry'
)
AS
BEGIN

	DELETE dbo.HolidayXCountry
	WHERE		HolidayXCountryId = ISNULL(@HolidayXCountryId, HolidayXCountryId)
	AND			HolidayId = ISNULL(@HolidayId, HolidayId)
	AND			CountryId = ISNULL(@CountryId, CountryId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @HolidayXCountryId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
