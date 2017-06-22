IF  EXISTS (SELECT * FROM sysobjects WHERE type = 'P' AND name ='SecurityXInvestmentIdentifierUpdate') 
BEGIN
	DROP Procedure SecurityXInvestmentIdentifierUpdate
END
GO

CREATE Procedure dbo.SecurityXInvestmentIdentifierUpdate
(
		@SecurityXInvestmentIdentifierId				INT
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
	,	@AuditId					INT
	,	@AuditDate					DATETIME		= NULL
	,	@SystemEntityType			VARCHAR(50)		= 'SecurityXInvestmentIdentifier'
)
AS
BEGIN

	UPDATE	dbo.SecurityXInvestmentIdentifier
	SET
			Ticker				=	@Ticker
		,	CUSIP				=	@CUSIP
		,	SEDOL				=	@SEDOL
		,	ISIN				=	@ISIN
		,	WKN				=	@WKN
		,	AltID1				=	@AltID1
		,	AltID2				=	@AltID2
		,	AltID3				=	@AltID3
		,	AltID4				=	@AltID4
		,	AltID5				=	@AltID5
		,	SecurityId				=	@SecurityId
	WHERE	SecurityXInvestmentIdentifierId			=   @SecurityXInvestmentIdentifierId

	EXEC dbo.AuditHistoryInsert 
			@SystemEntityType		= @SystemEntityType
		,	@EntityKey				= @SecurityXInvestmentIdentifierId
		,	@AuditAction			= 'Update'
		,	@CreatedDate			= @AuditDate
		,	@CreatedByPersonId		= @AuditId

END
GO
