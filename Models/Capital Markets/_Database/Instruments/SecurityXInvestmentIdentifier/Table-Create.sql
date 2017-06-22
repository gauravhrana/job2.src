IF OBJECT_ID ('dbo.SecurityXInvestmentIdentifier') IS NOT NULL
	DROP TABLE dbo.SecurityXInvestmentIdentifier
GO

CREATE TABLE dbo.SecurityXInvestmentIdentifier
(
		SecurityXInvestmentIdentifierId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	Ticker				VARCHAR(100)		NOT NULL
	,	CUSIP				VARCHAR(100)		NOT NULL
	,	SEDOL				VARCHAR(100)		NOT NULL
	,	ISIN				VARCHAR(100)		NOT NULL
	,	WKN				VARCHAR(100)		NOT NULL
	,	AltID1				INT		NOT NULL
	,	AltID2				INT		NOT NULL
	,	AltID3				INT		NOT NULL
	,	AltID4				INT		NOT NULL
	,	AltID5				INT		NOT NULL
	,	SecurityId				INT		NOT NULL
)
GO
