IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AddressInsert') 
BEGIN
	DROP Procedure AddressInsert
END
GO

CREATE Procedure dbo.AddressInsert
(
		@AddressId				INT		= NULL 	OUTPUT 
	,	@CityId				INT
	,	@StateId				INT
	,	@CountryId				INT
	,	@Address1				VARCHAR(500)
	,	@Address2				VARCHAR(500)
	,	@PostalCode				VARCHAR(500)
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Address'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL,@SystemEntityType,@AddressId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT

	SET @CreatedDate			= GETDATE()
	SET @UpdatedDate			= @CreatedDate
	SET @CreatedByAuditId		= @AuditId
	SET @ModifiedByAuditId		= @AuditId

	INSERT INTO dbo.Address
	(
			AddressId
		,	CityId
		,	StateId
		,	CountryId
		,	Address1
		,	Address2
		,	PostalCode
		,	ApplicationId
		,	CreatedDate
		,	UpdatedDate
		,	CreatedByAuditId
		,	ModifiedByAuditId
	)
	VALUES
	(
			@AddressId
		,	@CityId
		,	@StateId
		,	@CountryId
		,	@Address1
		,	@Address2
		,	@PostalCode
		,	@ApplicationId
		,	@CreatedDate
		,	@UpdatedDate
		,	@CreatedByAuditId
		,	@ModifiedByAuditId
	)

	SELECT @AddressId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @AddressId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
