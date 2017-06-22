IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='TimeZoneXCountryDelete') 
BEGIN
	DROP Procedure TimeZoneXCountryDelete
END
GO

CREATE Procedure dbo.TimeZoneXCountryDelete
(
		@TimeZoneXCountryId		INT
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'TimeZoneXCountry'
)
AS
BEGIN

	DELETE dbo.TimeZoneXCountry
	WHERE	TimeZoneXCountryId = @TimeZoneXCountryId

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @TimeZoneXCountryId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
