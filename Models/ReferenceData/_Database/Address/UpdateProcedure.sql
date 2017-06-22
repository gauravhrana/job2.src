IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='AddressUpdate') 
BEGIN
	DROP Procedure AddressUpdate
END
GO

CREATE Procedure dbo.AddressUpdate
(
		@AddressId				INT
	,	@CityId				INT
	,	@StateId				INT
	,	@CountryId				INT
	,	@Address1				VARCHAR(500)
	,	@Address2				VARCHAR(500)
	,	@PostalCode				VARCHAR(500)
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'Address'
)
AS
BEGIN

	DECLARE		@UpdatedDate		AS	 DATETIME
	DECLARE		@ModifiedByAuditId	AS	 INT

	SET			@UpdatedDate		= GETDATE()
	SET			@ModifiedByAuditId	= @AuditId

	UPDATE	dbo.Address SET
			CityId				=	@CityId
		,	StateId				=	@StateId
		,	CountryId				=	@CountryId
		,	Address1				=	@Address1
		,	Address2				=	@Address2
		,	PostalCode				=	@PostalCode
		,	UpdatedDate				=	@UpdatedDate
		,	ModifiedByAuditId		=   @ModifiedByAuditId	
	WHERE	AddressId			=   @AddressId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @AddressId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
