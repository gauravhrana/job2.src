IF OBJECT_ID ('dbo.SecurityXOther') IS NOT NULL
	DROP TABLE dbo.SecurityXOther
GO

CREATE TABLE dbo.SecurityXOther
(
		SecurityXOtherId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	SourcedFromThomsonReuters				VARCHAR(100)		NOT NULL
	,	WhenIssued				VARCHAR(100)		NOT NULL
	,	SecurityId				INT		NOT NULL
)
GO
