IF OBJECT_ID ('dbo.RecordType') IS NOT NULL
	DROP TABLE dbo.RecordType
GO

CREATE TABLE dbo.RecordType
(
		RecordTypeId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
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
