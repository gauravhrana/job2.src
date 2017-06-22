IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXInvestmentIdentifierInsert') 
BEGIN
	DROP Procedure SecurityXInvestmentIdentifierInsert
END
GO

CREATE Procedure dbo.SecurityXInvestmentIdentifierInsert
(
		@SecurityXInvestmentIdentifierId				INT		= NULL 	OUTPUT 
	,	@Ticker				VARCHAR(500)
	,	@CUSIP				VARCHAR(500)
	,	@SEDOL				VARCHAR(500)
	,	@ISIN				VARCHAR(500)
	,	@WKN				VARCHAR(500)
	,	@AltID1				INT
	,	@AltID2				INT
	,	@AltID3				INT
	,	@AltID4				INT
	,	@AltID5				INT
	,	@SecurityId				INT
	,	@ApplicationId				INT
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXInvestmentIdentifier'
)
AS
BEGIN

	EXEC dbo.SystemEntityTypeGetNextSequence NULL, @SystemEntityType, @SecurityXInvestmentIdentifierId Output, @AuditId

	DECLARE		@CreatedDate		AS		DATETIME
	DECLARE		@UpdatedDate		AS		DATETIME
	DECLARE		@CreatedByAuditId	AS		INT
	DECLARE		@ModifiedByAuditId	AS		INT


	INSERT INTO dbo.SecurityXInvestmentIdentifier
	(
			SecurityXInvestmentIdentifierId
		,	Ticker
		,	CUSIP
		,	SEDOL
		,	ISIN
		,	WKN
		,	AltID1
		,	AltID2
		,	AltID3
		,	AltID4
		,	AltID5
		,	SecurityId
		,	ApplicationId
	)
	VALUES
	(
			@SecurityXInvestmentIdentifierId
		,	@Ticker
		,	@CUSIP
		,	@SEDOL
		,	@ISIN
		,	@WKN
		,	@AltID1
		,	@AltID2
		,	@AltID3
		,	@AltID4
		,	@AltID5
		,	@SecurityId
		,	@ApplicationId
	)

	SELECT @SecurityXInvestmentIdentifierId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType	= @SystemEntityType
		,	@EntityKey			= @SecurityXInvestmentIdentifierId
		,	@AuditAction		= 'Insert'
		,	@CreatedDate		= @AuditDate
		,	@CreatedByPersonId	= @AuditId

END
GO
