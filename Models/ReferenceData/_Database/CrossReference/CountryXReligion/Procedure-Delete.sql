IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CountryXReligionDelete') 
BEGIN
	DROP Procedure CountryXReligionDelete
END
GO

CREATE Procedure dbo.CountryXReligionDelete
(
		@CountryXReligionId				INT		= NULL
	,	@CountryId				INT		= NULL
	,	@ReligionId				INT		= NULL
	,	@AuditId				INT
	,	@AuditDate				DATETIME = NULL
	,	@SystemEntityType		VARCHAR(50) = 'CountryXReligion'
)
AS
BEGIN

	DELETE dbo.CountryXReligion
	WHERE		CountryXReligionId = ISNULL(@CountryXReligionId, CountryXReligionId)
	AND			CountryId = ISNULL(@CountryId, CountryId)
	AND			ReligionId = ISNULL(@ReligionId, ReligionId)

	EXEC dbo.AuditHistoryInsert 
		@SystemEntityType	= @SystemEntityType
	,	@EntityKey			= @CountryXReligionId
	,	@AuditAction		= 'Delete'
	,	@CreatedDate		= @AuditDate
	,	@CreatedByPersonId	= @AuditId

END
GO
