IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TimeZoneXCountryUpdate') 
BEGIN
	DROP Procedure TimeZoneXCountryUpdate
END
GO

CREATE Procedure dbo.TimeZoneXCountryUpdate
(
		@TimeZoneXCountryId				INT
	,	@TimeZoneId				INT
	,	@CountryId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'TimeZoneXCountry'
)
AS
BEGIN

	UPDATE	dbo.TimeZoneXCountry
	SET
			TimeZoneId				=	@TimeZoneId
		,	CountryId				=	@CountryId
	WHERE	TimeZoneXCountryId			=   @TimeZoneXCountryId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @TimeZoneXCountryId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
