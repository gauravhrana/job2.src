IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='CountryXReligionUpdate') 
BEGIN
	DROP Procedure CountryXReligionUpdate
END
GO

CREATE Procedure dbo.CountryXReligionUpdate
(
		@CountryXReligionId				INT
	,	@CountryId				INT
	,	@ReligionId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'CountryXReligion'
)
AS
BEGIN

	UPDATE	dbo.CountryXReligion
	SET
			CountryId				=	@CountryId
		,	ReligionId				=	@ReligionId
	WHERE	CountryXReligionId			=   @CountryXReligionId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @CountryXReligionId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
