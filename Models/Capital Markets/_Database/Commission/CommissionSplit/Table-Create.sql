IF OBJECT_ID ('dbo.CommissionSplit') IS NOT NULL
	DROP TABLE dbo.CommissionSplit
GO

CREATE TABLE dbo.CommissionSplit
(
		CommissionSplitId				INT		NOT NULL
	,	ApplicationId				INT		NOT NULL
	,	CommissionSplitCode				VARCHAR(100)		NOT NULL
	,	CommissionSplitDescription				VARCHAR(100)		NOT NULL
	,	FullRate				DECIMAL(18, 5)		NOT NULL
	,	NoneCCA				DECIMAL(18, 5)		NOT NULL
	,	CCA				DECIMAL(18, 5)		NOT NULL
	,	StartDate				DATETIME		NULL
	,	EndDate				DATETIME		NULL
	,	LastModifiedBy				VARCHAR(100)		NOT NULL
	,	LastModifiedOn				DATETIME		NULL
	,	CommissionCodeId				INT		NOT NULL
)
GO
