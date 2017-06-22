IF OBJECT_ID ('dbo.Student') IS NOT NULL
	DROP TABLE dbo.Student
GO

CREATE TABLE dbo.Student
(
		StudentId			INT		NOT NULL
	,	ApplicationId			INT		NOT NULL
	,	Description				VARCHAR(100)		NOT NULL
	,	Name				VARCHAR(100)		NOT NULL
	,	SortOrder			INT		NOT NULL
	,	CreatedDate				DATETIME		NOT NULL
	,	UpdatedDate				DATETIME		NOT NULL
	,	CreatedByAuditId			INT		NOT NULL
	,	ModifiedByAuditId			INT		NOT NULL
)
GO
