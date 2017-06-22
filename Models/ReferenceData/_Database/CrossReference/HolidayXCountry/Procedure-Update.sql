IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='HolidayXCountryUpdate') 
BEGIN
	DROP Procedure HolidayXCountryUpdate
END
GO

CREATE Procedure dbo.HolidayXCountryUpdate
(
		@HolidayXCountryId				INT
	,	@HolidayId				INT
	,	@CountryId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'HolidayXCountry'
)
AS
BEGIN

	UPDATE	dbo.HolidayXCountry
	SET
			HolidayId				=	@HolidayId
		,	CountryId				=	@CountryId
	WHERE	HolidayXCountryId			=   @HolidayXCountryId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @HolidayXCountryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
