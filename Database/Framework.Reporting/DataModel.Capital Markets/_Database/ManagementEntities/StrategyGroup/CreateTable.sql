IF OBJECT_ID ('dbo.StrategyGroup') IS NOT NULL
	DROP TABLE dbo.StrategyGroup
GO

CREATE TABLE dbo.StrategyGroup
(
		StrategyGroupId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	FundId			INT		NOT NULL
	,	ClassificationId			INT		NOT NULL
	,	PortfolioId			INT		NOT NULL
	,	ParentStrategyGroupId			INT		NOT NULL
	,	DefaultUSecurityId			INT		NOT NULL
	,	ActiveYN			INT		NOT NULL
	,	OpenDateId			INT		NOT NULL
	,	CloseDateId			INT		NOT NULL
	,	ClosedYN			INT		NOT NULL
	,	ThemeId			INT		NOT NULL
	,	StrategyGroupCode				VARCHAR(100)		NOT NULL
	,	StrategyGroupDesc				VARCHAR(100)		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
