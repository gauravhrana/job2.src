IF OBJECT_ID ('dbo.FinancialAccountClass') IS NOT NULL
	DROP TABLE dbo.FinancialAccountClass
GO

CREATE TABLE dbo.FinancialAccountClass
(
		FinancialAccountClassId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	Assets				VARCHAR(100)		NOT NULL
	,	Liabilities				VARCHAR(100)		NOT NULL
	,	Equity				VARCHAR(100)		NOT NULL
	,	Income			INT		NOT NULL
	,	Expense				DECIMAL(18, 5)		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
