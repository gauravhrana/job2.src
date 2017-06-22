IF OBJECT_ID ('dbo.AgentBank') IS NOT NULL
	DROP TABLE dbo.AgentBank
GO

CREATE TABLE dbo.AgentBank
(
		AgentBankId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	Url				VARCHAR(100)		NOT NULL
	,	Code				VARCHAR(100)		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	SortOrder			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
